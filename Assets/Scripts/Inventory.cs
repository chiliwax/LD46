using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    [Space]
    [SerializeField] Transform ItemResult;
    private ItemSlot craft;

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
            Debug.Log(item.name+"\n");
        }
        craft.Item = Resources.Load<Item>("");
        RefreshUI();
    }

    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
