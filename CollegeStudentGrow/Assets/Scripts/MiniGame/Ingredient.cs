using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string Name;
    public Sprite Image;

    public Ingredient(string name, Sprite image)
    {
        Name = name;
        Image = image;
    }
}
