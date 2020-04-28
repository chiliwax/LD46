using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quests : ScriptableObject
{
    [Header("Quest Info")]
    public string QuestName;
    [TextArea(5, 20)]
    [Multiline]
    public string description;
    public bool IsLock;
    [Header("Requirement")]
    public int reputationMini;
    public int experienceMini;
    [Tooltip("LEGACY")]
    [Range(0, 30)]
    public int dificulty;

    [Header("Condition")]
    public bool AllowError = true;
    public Item[] ToAnswer;
    public Item[] ToNotAnswer;

    ///WIN AREA///
    [Header("Win")]
    [TextArea(5, 20)]
    [Multiline]
    public string WinDescription;
    public int WinReputation;
    public int WinExperience;
    public int WinOr;
    public Quests[] WinQuestUnlock;
    public Quests[] WinQuestlock;
    public Quests WinPlayAfter;
    public GameOver WinGameOver;
    ///NOTHING AREA///
    [Header("Nothing Happen")]
    [TextArea(5, 20)]
    [Multiline]
    public string NHDescription;
    public int NHReputation;
    public int NHExperience;
    public int NHOr;
    public Quests[] NHQuestUnlock;
    public Quests[] NHQuestlock;
    public Quests NHPlayAfter;
    public GameOver NHGameOver;
    ///LOOSE AREA///
    [Header("Loose")]
    [TextArea(5, 20)]
    [Multiline]
    public string LooseDescription;
    public int LooseReputation;
    public int LooseExperience;
    public int LooseOr;
    public Quests[] LooseQuestUnlock;
    public Quests[] LooseQuestlock;
    public Quests LoosePlayAfter;
    public GameOver LooseGameOver;

    [HideInInspector]
    public bool end = false;
    [HideInInspector]
    public bool IsWin = false;
    [HideInInspector]
    public bool IsDead = false;
    [HideInInspector]
    public List<Item> PlayerAnswer = new List<Item> { };
}
