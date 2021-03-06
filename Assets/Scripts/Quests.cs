﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quests : ScriptableObject
{
    public string QuestName;
    [TextArea]
    [Multiline]
    public string description;
    public int reward;
    public Item[] ToAnswer;
    public Item[] ToNotAnswer;
    [Range(0,30)]
    public int dificulty;
    public bool AllowError = true;

    [HideInInspector]
    public bool end = false;
    [HideInInspector]
    public bool IsWin = false;
    [HideInInspector]
    public bool IsDead = false;
    [HideInInspector]
    public List<Item> PlayerAnswer = new List<Item> {};
}
