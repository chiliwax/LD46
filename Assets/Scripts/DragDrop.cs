using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData evenData){
        Debug.Log("You drag ???.");
    }
    public void OnDrag(PointerEventData evenData){
        rectTransform.anchoredPosition += evenData.delta;
    }
    public void OnEndDrag(PointerEventData evenData){
        Debug.Log("You drop ???.");
    }
    public void OnPointerDown(PointerEventData evenData) {
        Debug.Log("You grab ???.");
    }
}
