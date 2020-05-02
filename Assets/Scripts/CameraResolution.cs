using UnityEngine;
using System;

public class CameraResolution : MonoBehaviour
{
    [Header("Resolution Ratio")]
    [SerializeField] private float RatioX = 16.0f;
    [SerializeField] private float RatioY = 9.0f;
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;
    private Camera MainCamera ;

    private void ResizeCamera()
    {
        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = RatioX / RatioY;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        //Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = MainCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            MainCamera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = MainCamera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            MainCamera.rect = rect;
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }

    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }

    // Use this for initialization
    void Start()
    {
        MainCamera = GetComponent<Camera>();
        ResizeCamera();
    }

    // Update is called once per frame
    void Update()
    {
        ResizeCamera();
    }
}
