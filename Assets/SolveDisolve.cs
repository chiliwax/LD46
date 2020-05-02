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
  //  private Image[] images;
    private enum status { solve, disolve, destroy, appear };

    void Start()
    {
   //     images = gameObject.GetComponentsInChildren<Image>();
        mat = gameObject.GetComponentInChildren<Image>().material;
        if (!mat)
        {
   //         images = gameObject.GetComponents<Image>();
            mat = gameObject.GetComponent<Image>().material;
        }
        if (DisplayStart)
        {
       //     foreach (var item in images) {item.enabled = true;};
            mat.SetFloat("_Fade", 1);
        }
        else
        {
     //       foreach (var item in images) {item.enabled = false;};
            mat.SetFloat("_Fade", 0);
        }
    }

    public void solve()
    {
       if ( !gameObject.GetComponentInChildren<Image>() && !gameObject.GetComponent<Image>()) {
           return;
       }
        if (mat.GetFloat("_Fade") == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(status.solve));
        }
    }
    public void disolve()
    {
        if ( !gameObject.GetComponentInChildren<Image>() && !gameObject.GetComponent<Image>()) {
           return;
       }
        if (mat.GetFloat("_Fade") == 1)
        {
            StopAllCoroutines();
            StartCoroutine(Exec(status.disolve));
        }
    }

    public void destroy()
    {
        if ( !gameObject.GetComponentInChildren<Image>() && !gameObject.GetComponent<Image>()) {
           return;
       }
        mat.SetFloat("_Fade", 0);
     //   foreach (var item in images) {item.enabled = false;};
    }

    public void apear()
    {
        if ( !gameObject.GetComponentInChildren<Image>() && !gameObject.GetComponent<Image>()) {
           return;
       }
        mat.SetFloat("_Fade", 1);
    //    foreach (var item in images) {item.enabled = true;};
    }

    public bool isVisible() {
        return (mat.GetFloat("_Fade") != 0);
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
  //          foreach (var item in images) {item.enabled = true;};
        }
        else if (_status == status.disolve)
        {
            for (float i = 1; i > 0; i -= definition)
            {
                mat.SetFloat("_Fade", i);
                yield return new WaitForSeconds(speed);
            }
            mat.SetFloat("_Fade", 0);
   //         foreach (var item in images) {item.enabled = false;};
        }
    }

}
