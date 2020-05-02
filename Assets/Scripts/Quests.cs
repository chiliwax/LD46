using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quests : ScriptableObject
{
    [Header("Quest Info")]
    public string QuestName = "";
    public string ENQuestName = "";
    [TextArea(5, 20)]
    [Multiline]
    public string description = "";
    [TextArea(5, 20)]
    [Multiline]
    public string ENDescription = "";
    public bool IsLock;
    [Header("Requirement")]
    public int reputationMini = 0;
    public int experienceMini = 0;
    [Tooltip("LEGACY")]
    [Range(0, 30)]
    public int dificulty = 0;

    [Header("Condition")]
    public bool AllowError = true;
    public Item[] ToAnswer;
    public Item[] ToNotAnswer;

    ///WIN AREA///
    [Header("Win")]
    [TextArea(5, 20)]
    [Multiline]
    public string WinDescription = "";
    [TextArea(5, 20)]
    [Multiline]
    public string ENWinDescription = "";
    [Range(-100,100)]
    public int WinReputation = 0;
    [Range(0,100)]
    public int WinExperience = 0;
    [Range(-100,100)]
    public int WinOr = 0;
    public Quests[] WinQuestUnlock;
    public Quests[] WinQuestlock;
    public Quests WinPlayAfter;
    public GameOver WinGameOver;
    ///NOTHING AREA///
    [Header("Nothing Happen")]
    [TextArea(5, 20)]
    [Multiline]
    public string NHDescription = "";
    [TextArea(5, 20)]
    [Multiline]
    public string ENNHDescription = "";
    [Range(-100,100)]
    public int NHReputation = 0;
    [Range(0,100)]
    public int NHExperience = 0;
    [Range(-100,100)]
    public int NHOr = 0;
    public Quests[] NHQuestUnlock;
    public Quests[] NHQuestlock;
    public Quests NHPlayAfter;
    public GameOver NHGameOver;
    ///LOOSE AREA///
    [Header("Loose")]
    [TextArea(5, 20)]
    [Multiline]
    public string LooseDescription = "";
    [TextArea(5, 20)]
    [Multiline]
    public string ENLooseDescription = "";
    [Range(-100,100)]
    public int LooseReputation = 0;
    [Range(0,100)]
    public int LooseExperience = 0;
    [Range(-100,100)]
    public int LooseOr = 0;
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
