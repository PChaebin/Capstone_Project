using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ Ŭ���� (������ ������ ���� ����)
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

        recipeSteps.text += string.Join("\n", drink.Recipes);

        Debug.Log($"Attempting to load sprite from path: {drink.FinalImage}");

        selectImage(drink);
    }

    private void selectImage(Drinks drink)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(drink.FinalImageFile);

        Sprite finalSprtie = System.Array.Find<Sprite>(sprites, sprite => sprite.name == drink.FinalImage);
        recipeFinalImg.sprite = finalSprtie;

        recipeFinalImg.SetNativeSize();

        RectTransform rectTransform = recipeFinalImg.GetComponent<RectTransform>();

        float scaleFactor = 0.4f; // ���ϴ� ũ�� ���� ����
        rectTransform.sizeDelta = rectTransform.sizeDelta * scaleFactor;
    }
}
