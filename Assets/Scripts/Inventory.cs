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
        foreach (var item in items)
        {
            Debug.Log(item.name + "\n");
        }

        foreach (var recipe in recipes)
        {
            if (recipe.items.Length == items.Count)
            {
                Array.Sort(recipe.items);
                items.Sort();
                Debug.Log(recipe.items[0].name + " " + items[0].name);
                Debug.Log(recipe.items[1].name + " " + items[1].name);
                Debug.Log(recipe.items[2].name + " " + items[2].name);
                Debug.Log(recipe.items[3].name + " " + items[3].name);
                // if (recipe.items[0].name.ToString() == items[0].name.ToString() &&
                //     recipe.items[1].name.ToString() == items[1].name.ToString())
                // {
                //     craft.Item = recipe.result;
                // }
            }
            craft.Item = null;

        }

        RefreshUI();
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
