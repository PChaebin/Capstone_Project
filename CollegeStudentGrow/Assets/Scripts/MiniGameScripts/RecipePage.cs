using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 레시피 클래스 (각각의 레시피 관리 가능)
/// </summary>
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
