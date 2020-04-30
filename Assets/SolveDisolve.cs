using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolveDisolve : MonoBehaviour
{
    public bool DisplayStart = true;
    public float speed = 0.01f;
    [Range(0.01f, 0.1f)]
    public float definition = 0.05f;
    private Material mat;

    private enum status { solve, disolve, destroy };

    void Start()
    {
        mat = gameObject.GetComponent<Image>().material;
        if (DisplayStart)
        {
            mat.SetFloat("_Fade", 1);
        }
        else
        {
            mat.SetFloat("_Fade", 0);
        }
    }

    public void solve()
    {
        if (mat.GetFloat("_Fade") == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(status.solve));
        }
    }
    public void disolve()
    {
        if (mat.GetFloat("_Fade") == 1)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(status.disolve));
        }
    }

    public void destroy()
    {
        if (mat.GetFloat("_Fade") == 1)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(status.destroy));
        }
    }

    IEnumerator Exec(status _status)
    {
        if (_status == status.solve)
        {
            for (float i = 0; i < 1; i += definition)
            {
                mat.SetFloat("_Fade", i);
                yield return new WaitForSeconds(speed);
            }
            mat.SetFloat("_Fade", 1);
        }
        else if (_status == status.disolve)
        {
            for (float i = 1; i > 0; i -= definition)
            {
                mat.SetFloat("_Fade", i);
                yield return new WaitForSeconds(speed);
            }
            mat.SetFloat("_Fade", 0);
        }
        else
        {
            mat.SetFloat("_Fade", 0);
        }
    }

}
