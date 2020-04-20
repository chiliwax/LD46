using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public EndGame endGame_class;
    public GameObject sendbutton;
    public GameObject EndMissionScreen;
    public GameObject EndGameScreen;
    public TimeManager timemanager;
    private List<Quests> FinishQuests = new List<Quests> { };
    [HideInInspector]
    public Quests[] quests;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    [HideInInspector]
    public Quests activeQuest;

    void Start()
    {
        quests = Resources.LoadAll<Quests>("Quests");
        ResetQuests();
        NewQuest();
    }

    void ResetQuests()
    {
        foreach (var item in quests)
        {
            item.end = false;
            item.PlayerAnswer.RemoveRange(0, item.PlayerAnswer.Count);
            item.IsWin = false;
            item.IsDead = false;
        }
    }

    public void NewQuest()
    {
        List<Quests> available_quests = new List<Quests> { };
        foreach (var item in quests)
        {
            //All conditions
            if (item.end == false && item.dificulty <= timemanager.getWichDay())
            {
                Debug.Log("add");
                available_quests.Add(item);
            }
        }
        Debug.Log("available quests " + available_quests.Count);

        int deaths = 0;
        int win = 0;
        int loose = 0;

        foreach (var item in quests)
        {
            if (item.IsDead) { deaths += 1; }
            if (item.IsWin) { win += 1; } else { loose += 1; }
        }
        if (available_quests.Count == 0)
        {
            foreach (var item in quests)
            {
                if (item.end == false)
                {
                    timemanager.IncraseDay();
                }
                else
                {
                    EndGame(deaths, win, loose);
                }
            }
            return;
        }
        if (deaths >= 5)
        {
            EndGame(deaths, win, loose);
            return;
        }
        activeQuest = available_quests[Random.Range(0, available_quests.Count)];

        StopAllCoroutines();
        title.text = activeQuest.QuestName;
        sendbutton.SetActive(false);
        StartCoroutine(TypeSentence(activeQuest.QuestName, title));
        StartCoroutine(TypeSentence(activeQuest.description, description));
        // title.text = activeQuest.QuestName;
        // description.text = activeQuest.description;
        return;
    }

    private void checkWin()
    {
        List<string> list1 = new List<string> { };
        List<string> list2 = new List<string> { };

        foreach (var item in activeQuest.ToNotAnswer)
        {
            if (activeQuest.PlayerAnswer.Contains(item))
            {
                activeQuest.IsWin = false;
                activeQuest.IsDead = true;
                return;
            }
        }

        foreach (var item in activeQuest.PlayerAnswer)
        {
            list1.Add(item.objectName);
            list1.Sort();
        }

        foreach (var item in activeQuest.ToAnswer)
        {
            list2.Add(item.objectName);
            list2.Sort();
        }

        if (list1.Count >= list2.Count)
        {
            int i = 0;
            int n = 0;

            while (i < list2.Count)
            {
                if (n >= list1.Count)
                {
                    activeQuest.IsWin = false;
                    activeQuest.IsDead = false;
                    return;
                }
                if (list1[n] == list2[i])
                {
                    i++;
                    n++;
                }
                else n++;
            }
            activeQuest.IsWin = true;
            activeQuest.IsDead = false;
        }
        else
        {
            activeQuest.IsWin = false;
            activeQuest.IsDead = false;
            return;
        }
    }

    public void EndQuest()
    {
        checkWin();
        Debug.Log("Win : " + activeQuest.IsWin);
        Debug.Log("Dead : " + activeQuest.IsDead);
        Debug.Log("Nb compo inside : " + activeQuest.PlayerAnswer.Count);
        FinishQuests.Add(activeQuest);
        title.text = null;
        description.text = null;
        foreach (var item in quests)
        {
            if (item.QuestName == activeQuest.QuestName)
            {
                item.end = true;
            }
        }
        EndMissionScreen.SetActive(true);
        EndMissionScreen.GetComponent<EndMission>().UpdateRecap();
        // NewQuest();
    }

    public void AddItemInQuest(ItemSlot item)
    {
        activeQuest.PlayerAnswer.Add(item.Item);
    }

    void EndGame(int deaths, int win, int loose)
    {
        Debug.Log("GameFinish !");
        EndGameScreen.SetActive(true);

        if (deaths >= 5)
        {
            Debug.Log("To Many deaths");

        }
        else
        {
            Debug.Log("WinWin");
        }
        endGame_class.UpdateRecap();
    }

    Quests getActiveQuest()
    {
        return activeQuest;
    }

    IEnumerator TypeSentence(string text, TextMeshProUGUI mesh)
    {
        mesh.text = "";
        foreach (char letter in text)
        {
            sendbutton.SetActive(false);
            mesh.text += letter;
            yield return new WaitForSeconds(0.0005f);
            //yield return null;
        }
        sendbutton.SetActive(true);
    }
}
