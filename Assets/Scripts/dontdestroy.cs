using UnityEngine;

public class dontdestroy : MonoBehaviour
{
private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
