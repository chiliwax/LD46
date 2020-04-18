using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class description : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI infos;
    void Start() {
        gameObject.SetActive(false);
    }
    public void enterD(Item item)
    {
        gameObject.SetActive(true);
        
        title.text = item.objectName;
        infos.text = item.description;
        return;
    }

    public void exitD(Item item)
    {
        gameObject.SetActive(false);
        return;
    }
}
