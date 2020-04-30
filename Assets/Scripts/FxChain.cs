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
        Transform launcher;
        launcher = FX(transform);
        while (launcher != null)
        {
            Transform oldLauncher = launcher;
            launcher = FX(oldLauncher);
        }
    }
    private Transform FX(Transform launcher)
    {
        child = launcher.GetChild(0);
        if (child != null)
        {
            if (child.GetComponent<ParticleSystem>())
                child.GetComponent<ParticleSystem>().Play();
        }
        return child;
    }


}
