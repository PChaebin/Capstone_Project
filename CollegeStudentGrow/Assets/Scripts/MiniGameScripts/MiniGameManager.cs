using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private RecipeManager recipeManager;
    [SerializeField] private RecipeUIManager recipeUIManager;

    private void Start()
    {
        recipeManager.LoadRecipesToJson(); // JSON ������ �ε�

        List<Drinks> randomDrinks = recipeManager.GetRandomRecipes(); // ���� ���� 3�� ��������

        Debug.Log("drinks ���� : " +  randomDrinks.Count);

        if (randomDrinks != null)
        {
            recipeUIManager.showRecipesUI(randomDrinks); // UI�� ǥ��
        }
    }
}
