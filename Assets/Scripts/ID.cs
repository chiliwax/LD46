using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ID : MonoBehaviour
{
    [SerializeField]  public string name_item;
    [SerializeField] public string description_item;
    [SerializeField]  public int id_item;

    [SerializeField] public Canvas canvas; //canva maitre (doit etre en 1ER plan)
    [SerializeField] public GameObject objectParent; //parent de l'objet

    [SerializeField] public Boolean show_description = true;

    private void OnMouseOver()
    {
        
    }
}
