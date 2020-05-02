using UnityEngine;

public class FxChain : MonoBehaviour
{
    public void PlayFX()
    {
        ParticleSystem rooteffect;
        if (rooteffect = transform.GetComponent<ParticleSystem>()) { rooteffect.Play(); }
        foreach (var item in transform.GetComponentsInChildren<ParticleSystem>()) { item.Play(); }
    }
}
