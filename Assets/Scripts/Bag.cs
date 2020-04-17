using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour, IDropHandler
{
    [SerializeField] public Boolean unlock = true;
    [SerializeField] public Boolean DEV_Comment = false;
    public Boolean empty = true;
    public GameObject butin;

    public void OnDrop(PointerEventData eventData)
    {
        if (DEV_Comment) Debug.Log("drop in bag");
        if (eventData.pointerDrag != null & empty & unlock)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragDrop>().inThePocket = true;
            eventData.pointerDrag.GetComponent<DragDrop>().pocket = gameObject;
            empty = false;

            if (DEV_Comment) Debug.Log("bag is full");
            butin = eventData.pointerDrag;
        }
    }
}
