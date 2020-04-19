using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private List<Quests> FinishQuests = new List<Quests> { };
    public Quests[] quests;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    private Quests activeQuest;

    void Start()
    {
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
            if (item.end == false)
            {
                Debug.Log("add");
                available_quests.Add(item);
            }
        }
        Debug.Log("available quests " + available_quests.Count);
        if (available_quests.Count == 0)
        {
            EndGame();
            return;
        }
        activeQuest = available_quests[Random.Range(0, available_quests.Count)];

        StopAllCoroutines();
        title.text = activeQuest.QuestName;
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
                } else n++;
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
        // if (list1.Count == list2.Count) {
        //     for (int i = 0; i < list1.Count; i++)
        //         {
        //             if (list1[i] != list2[i]) {
        //                 activeQuest.IsWin = false;
        //                 activeQuest.IsDead = false;
        //                 return;
        //             }
        //         }
        //     activeQuest.IsWin = true;
        //     activeQuest.IsDead = false;
        // }
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
        NewQuest();
    }

    public void AddItemInQuest(ItemSlot item)
    {
        activeQuest.PlayerAnswer.Add(item.Item);
    }

    void EndGame()
    {
        Debug.Log("GameFinish !");
    }

    IEnumerator TypeSentence(string text, TextMeshProUGUI mesh)
    {
        mesh.text = "";
        foreach (char letter in text)
        {
            mesh.text += letter;
            yield return new WaitForSeconds(0.025f);
            //yield return null;
        }
    }
}
