using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipePage : MonoBehaviour
{
    public TextMeshProUGUI recipeName;
    public Image recipeFinalImg;
    public TextMeshProUGUI recipeDescription;

    public void SetPage(Drinks drink)
    {
        recipeName.text = drink.Name;
        recipeDescription.text = drink.Description;

        Sprite sprite = Resources.Load<Sprite>(drink.FinalImage);
        recipeFinalImg.sprite = sprite;
    }
}
