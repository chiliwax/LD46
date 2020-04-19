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
    [Range(1,5)]
    public int dificulty;

    [HideInInspector]
    public bool end = false;
}
