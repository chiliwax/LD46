using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public void hightlightIt(SpriteRenderer mySprite)
    {
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,0.7f);
    }
    public void UnhightlightIt(SpriteRenderer mySprite)
    {
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,1f);
    }

    public void hightlightimageIt(Image mySprite) {
         mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,0.7f);
    }
    public void UnhightlightimageIt(Image mySprite) {
        mySprite.color = new Color(mySprite.color.r,mySprite.color.g,mySprite.color.b,1f);
    }
}
