using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    void OnPreCull() => GL.Clear(true, true, Color.black);
}

