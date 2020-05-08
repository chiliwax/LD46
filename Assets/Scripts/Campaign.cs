using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Campaign : ScriptableObject
{
    public List<Quests> GetQuests()
    {
        Quests[] AllQuests = Resources.LoadAll<Quests>("");
        List<Quests> quests = new List<Quests>();
        foreach (Quests q in AllQuests)
        {
            if (q.campagne == this) quests.Add(q);
        }
        return quests;
    }
}
