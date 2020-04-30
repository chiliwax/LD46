using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolveDisolve : MonoBehaviour
{
    public float speed = 0.01f;
    [Range(0.01f,0.1f)]
    public float definition = 0.05f;
    private Material mat;

    void Start()
    {
        mat = gameObject.GetComponent<Image>().material;
        mat.SetFloat("_Fade", 0);
    }

    public void solve()
    {
        if (mat.GetFloat("_Fade") == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(true));
        }
    }
    public void disolve()
    {
        if (mat.GetFloat("_Fade") == 1)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(false));
        }
    }

    IEnumerator Exec(bool Craft)
    {
        if (Craft)
        {
            for (float i = 0; i < 1; i += definition)
            {
                mat.SetFloat("_Fade", i);
                yield return new WaitForSeconds(speed);
            }
            mat.SetFloat("_Fade", 1);
        }
        else
        {
            // for (float i = 1; i > 0; i -= definition)
            // {
            //     mat.SetFloat("_Fade", i);
            //     yield return new WaitForSeconds(speed);
            // }
            mat.SetFloat("_Fade", 0);
        }
    }

}
