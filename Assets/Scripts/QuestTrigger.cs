using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTrigger : ScriptableObject
{

    [Header("Trigger Condition (AND Condition)")]
    public bool Or;
    public int QtOr;
    public bool Reputation;
    public int QTReputation;
    public bool Death;
    public int QTDeath;
    public bool Experience;
    public int QTExperience;
    public bool QuestWin;
    public int QTQuestWin;
    public bool QuestNothing;
    public int QTQuestNothing;
    public bool QuestLoose;
    public int QTQuestLoose;
    [Header("Quest To Launch")]
    public Quests quest;
}
