using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Linq;
public class Crafter : MonoBehaviour
{
    private recipes[] recipes;
    [SerializeField] [HideInInspector] List<Item> items;
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
        if (!craft) { craft = ItemResult.GetComponent<ItemSlot>(); }
        craft.Item = null;
        itemsParent.GetComponent<SolveDisolve>().disolve();
        List<Item> OrderCrafted = items.OrderBy(o=>o.objectName).ToList();

        items.RemoveRange(0, items.Count);
        Debug.Log("NBreceip : " + recipes.Length.ToString());
        foreach (var recipe in recipes)
        {
            List<Item> OrderRecipe = recipe.items.OrderBy(o=>o.objectName).ToList();
            if (OrderCrafted.SequenceEqual(OrderRecipe))
            {
                craft.Item = recipe.result;
                craft.GetComponentInParent<SolveDisolve>().solve();
            }
        }
        //UI will be refresh when we add items again. (allow desolve)
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
