using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Inventory : MonoBehaviour
{
    private recipes[] recipes;
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    [Space]
    [SerializeField] Transform ItemResult;
    [SerializeField] GameObject[] ToHide;
    [SerializeField] GameObject[] ToShow;
    [SerializeField] CanvasGroup[] RaycastToDesactivate;
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
            list1.Add(item.objectName);
            list1.Sort();
        }
        Debug.Log("NBreceip : "+recipes.Length.ToString());
        foreach (var recipe in recipes)
        {
            list2.RemoveRange(0, list2.Count);
            foreach (var item in recipe.items)
            {
                list2.Add(item.objectName);
                list2.Sort();
            }

            if (list1.Count == list2.Count)
            {
                Debug.Log("Recette same size");
                bool pass = true;
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i] != list2[i])
                    {
                        items.RemoveRange(0, items.Count);
                        RefreshUI();
                        pass = false;
                    }
                }
                if (pass == false) {continue;}
                Debug.Log("Receip exist");
                craft.Item = recipe.result;
                foreach (var item in ToHide)
                    item.SetActive(false);
                foreach (var item in ToShow)
                    item.SetActive(true);
                foreach (var item in RaycastToDesactivate)
                {
                    item.blocksRaycasts = false;
                }
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
