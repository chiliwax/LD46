using UnityEngine;

public class FxChain : MonoBehaviour
{
    public void PlayFX()
    {
        ParticleSystem rootEffect;
        if (rootEffect = transform.GetComponent<ParticleSystem>()) { rootEffect.Play(); }
        foreach (var item in transform.GetComponentsInChildren<ParticleSystem>()) { item.Play(); }
    }
}
