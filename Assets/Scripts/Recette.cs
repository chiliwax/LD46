using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recette : MonoBehaviour
{
    
    private string[] slot;
    private void CraftItem(string item) //recette à deux éléments, possibilité de surcharge pour plus de combinaisons
    {
        // instancie l'item "craft"
        GameObject itemCraft = (GameObject)Instantiate(Resources.Load(item));
        itemCraft.transform.parent = GameObject.FindGameObjectWithTag("MasterCanva").GetComponent<Canvas>().transform;
        itemCraft.transform.localPosition = transform.localPosition;

        // destroy les items de la recette
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Bag>() != null)
                if (child.GetComponent<Bag>().butin != null)
                {
                    child.GetComponent<Bag>().butin.GetComponent<ID>().kill();
                    i++;
                }
        }
    }

    public void Craft()
    {
        // clean
        string[] slot = new string[2];


        // renseigne les items present dans les slots de craft
        int i = 0;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Bag>() != null)
                if (child.GetComponent<Bag>().butin != null)
                {
                    slot[i] = child.GetComponent<Bag>().butin.GetComponent<ID>().name_item;
                    i++;
                }
        }


        // regarde si les slots sont remplie et si oui regarde si une recette match
        if (slot[0] != null & slot[1] != null) 
        {
            //Recette exemple
            if (slot[0] == "nom de l'objet1" & slot[1] == "nom de l'objet2") CraftItem("nom du Prefab de l'objet à créer"); 
            //Liste Recette
            if (slot[0] == "Gemme rutilante" & slot[1] == "Branche longue") CraftItem("Baguette_01");


        }
    

     }   
        
}
            

