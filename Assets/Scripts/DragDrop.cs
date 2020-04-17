using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    private RectTransform rectTransform;
    private Vector3 localPositionInitial;
    private CanvasGroup canvasGroup;
    public Boolean inThePocket = false;
    public GameObject pocket= null;
    [SerializeField] public Boolean DEV_Comment = false;

    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();    
    }
    public void OnBeginDrag(PointerEventData evenData){
        if (DEV_Comment) Debug.Log("You drag "+ GetComponent<ID>().name_item + ".");
        canvasGroup.alpha = .9f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData evenData) => rectTransform.anchoredPosition += evenData.delta / GetComponent<ID>().canvas.scaleFactor;
    public void OnEndDrag(PointerEventData evenData){
        if (inThePocket == false) // Si l'ojet n'est pas mis dans un inventaire > retour a son emplacement et canvas d'origine
        {
            if (DEV_Comment) Debug.Log("You drop " + GetComponent<ID>().name_item + ".");
            transform.parent = GetComponent<ID>().objectParent.transform;
            rectTransform.localPosition = localPositionInitial;
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        
    }
     public void OnPointerDown(PointerEventData evenData) {
        if (DEV_Comment) Debug.Log("You grab " + GetComponent<ID>().name_item + ".");
        Debug.Log(gameObject.name);
        if (inThePocket == false) localPositionInitial = rectTransform.localPosition; //sauvegarde sa position d'origine sur la map
        inThePocket = false;
        if (pocket != null)
        {
            pocket.GetComponent<Bag>().empty = true;
            pocket.GetComponent<Bag>().butin = null;
            pocket = null;
        }
        transform.parent = GetComponent<ID>().canvas.transform; // l'objet est mis sur le canvas maitre, donc un premier plan

    }
    public void OnDrop(PointerEventData evenData) { }
}
