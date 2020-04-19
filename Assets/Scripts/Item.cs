using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
  public string objectName;
  [TextArea]
  public string description;
  public Sprite Icon;
}
