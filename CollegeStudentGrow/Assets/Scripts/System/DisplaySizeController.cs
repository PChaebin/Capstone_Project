using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySizeController : MonoBehaviour
{
    private void Update()
    {
        DisplaySize();
    }

    private void DisplaySize()
    {
        var camera = GetComponent<Camera>();
        var rect = camera.rect;
        var scaleHeight = ((float)Screen.width / Screen.height) / (9f / 16f);
        var scaleWidth = 1f / scaleHeight;

        if(scaleHeight < 1f)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}
