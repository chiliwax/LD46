using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public void hightlightIt()
    {
        var mySprite = transform.GetComponent<SpriteRenderer>();
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,0.7f);
    }
    public void UnhightlightIt()
    {
        var mySprite = transform.GetComponent<SpriteRenderer>();
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,1f);
    }

    public void hightlightimageIt() {
        var mySprite = transform.GetComponent<Image>();
         mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,0.7f);
    }
    public void UnhightlightimageIt() {
        var mySprite = transform.GetComponent<Image>();
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,1f);
    }
}
