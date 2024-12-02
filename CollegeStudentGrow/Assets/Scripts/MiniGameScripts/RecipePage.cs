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
    public TextMeshProUGUI recipeSteps;

    public void SetPage(Drinks drink)
    {
        recipeName.text = drink.Name;
        recipeDescription.text = drink.Description;

        recipeSteps.text += string.Join("\n", drink.steps);

        Debug.Log($"Attempting to load sprite from path: {drink.FinalImage}");

        selectImage(drink);
    }

    private void selectImage(Drinks drink)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(drink.FinalImageFile);

        Sprite finalSprtie = System.Array.Find<Sprite>(sprites, sprite => sprite.name == drink.FinalImage);
        recipeFinalImg.sprite = finalSprtie;
    }
}
