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
        NewQuest();
    }


    public void NewQuest()
    {
        List<Quests> available_quests = new List<Quests> { };
        foreach (var item in quests)
        {
            //All conditions
            if (item.end == false) {
                available_quests.Add(item);
            }
        }
        if(available_quests.Count == 0) {
            EndGame();
            return;
        }
        activeQuest = available_quests[Random.Range(0, available_quests.Count)];
        title.text = activeQuest.QuestName;
        description.text = activeQuest.description;
        return;
    }

    void EndQuest()
    {
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
