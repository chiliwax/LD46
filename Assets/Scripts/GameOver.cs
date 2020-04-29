using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameOver : ScriptableObject
{
    public string title;
    [TextArea(5,20)]
    public string description;
    public Sprite background;
}
