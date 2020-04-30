using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxChain : MonoBehaviour
{
    private Transform child;

    private void Awake()
    {
        child = transform.GetChild(0);
    }
    public void PlayFX()
    {
        
        if (child != null)
        {
            if (child.GetComponent<ParticleSystem>())
                child.GetComponent<ParticleSystem>().Play();
            if (child.GetComponent<FxChain>())
                child.GetComponent<FxChain>().PlayFX();
        }
    }


}
