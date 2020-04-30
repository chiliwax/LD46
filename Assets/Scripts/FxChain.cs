using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxChain : MonoBehaviour
{
    private Transform child;

   
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
        if (launcher.childCount > 0)
        {
            child = launcher.GetChild(0);
            if (child != null)
            {
                if (child.GetComponent<ParticleSystem>())
                    child.GetComponent<ParticleSystem>().Play();
            }
        }
        else child = null;
        return child;
    }


}
