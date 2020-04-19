using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public Quests[] quests;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    private Quests activeQuest;

    void Start()
    {
        
        ResetQuests();
        NewQuest();
    }

    void ResetQuests(){
        foreach (var item in quests)
        {
            item.end = false;
        }
    }

    public void NewQuest()
    {
        List<Quests> available_quests = new List<Quests> { };
        foreach (var item in quests)
        {
            //All conditions
            if (item.end == false) {
                Debug.Log("add");
                available_quests.Add(item);
            }
        }
        Debug.Log("available quests " + available_quests.Count);
        if(available_quests.Count == 0) {
            EndGame();
            return;
        }
        activeQuest = available_quests[Random.Range(0, available_quests.Count)];

        
        title.text = activeQuest.QuestName;
        description.text = activeQuest.description;
        return;
    }

    public void EndQuest()
    {
        title.text = null;
        description.text = null;
        foreach (var item in quests)
        {
            if (item.QuestName == activeQuest.QuestName) {
                item.end = true;
            }
        }
        NewQuest();
    }

    void EndGame()
    {
        Debug.Log("GameFinish !");
    }
}
