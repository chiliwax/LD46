using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class inventory
{
    private static int TotalSlots = 10;
    private static int UsedSlots = 0;
    private static List<string> items;

    public bool additem(string item)
    {
        if (UsedSlots < TotalSlots)
        {
            items.Add(item);
            return true;
        }
        return false;
    }
    public int getFreeSpace()
    {
        return (TotalSlots - UsedSlots);
    }

    public List<string> getItems()
    {
        return items;
    }

}