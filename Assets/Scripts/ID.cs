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

    [SerializeField] public Boolean show_description = true;

    public Canvas canvas; //canva maitre (doit etre en 1ER plan)
    public GameObject objectParent; //parent de l'objet

    

    private void Start()
    {
        objectParent = transform.parent.gameObject;
        canvas = GameObject.FindGameObjectWithTag("MasterCanva").GetComponent<Canvas>();
    }

    public void kill()
    {
        GameObject.Destroy(gameObject, 0f);
    }
}
