using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class inventory : MonoBehaviour
{
    public recipes[] recipes;
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    [Space]
    [SerializeField] Transform ItemResult;
    private ItemSlot craft;

    private void Start()
    {
        recipes = Resources.LoadAll<recipes>("Recipes");
    }

    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
        RefreshUI();
    }
    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public void AddItem(Item item)
    {
        if (IsFull())
            return;
        items.Add(item);
        RefreshUI();
        return;
    }

    public void RemoveItem(int i)
    {
        if (items.Remove(items[i]))
        {
            RefreshUI();
            return;
        }
        return;
    }

    public void Craft()
    {
        craft = ItemResult.GetComponentInChildren<ItemSlot>();
        craft.Item = null;

        List<String> list1 = new List<String> { };
        List<String> list2 = new List<String> { };

        foreach (var item in items)
        {
            list1.Add(item.name);
            list1.Sort();
        }

        foreach (var recipe in recipes)
        {
            list2.RemoveRange(0,list2.Count);
            foreach (var item in recipe.items)
            {
                list2.Add(item.name);
                list2.Sort();
            }

            if (list1.Count == list2.Count)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i] != list2[i])
                    {
                        items.RemoveRange(0, items.Count);
                        RefreshUI();
                        return;
                    }
                }
                craft.Item = recipe.result;
            }
        }
        items.RemoveRange(0, items.Count);
        RefreshUI();
        return;
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
