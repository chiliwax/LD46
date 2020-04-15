using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform rectTransform;
    private Vector3 localPositionInitial;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData evenData){
        Debug.Log("You drag "+ GetComponent<ID>().name_item + ".");
    }
    public void OnDrag(PointerEventData evenData) => rectTransform.anchoredPosition += evenData.delta / GetComponent<ID>().C_VT.scaleFactor;
    public void OnEndDrag(PointerEventData evenData){
        Debug.Log("You drop " + GetComponent<ID>().name_item + ".");
        transform.parent = GetComponent<ID>().objectParent.transform;
        rectTransform.localPosition = localPositionInitial;
    }
    public void OnPointerDown(PointerEventData evenData) {
        Debug.Log("You grab " + GetComponent<ID>().name_item + ".");
        localPositionInitial = rectTransform.localPosition;
        transform.parent = GetComponent<ID>().C_VT.transform;

    }
}
