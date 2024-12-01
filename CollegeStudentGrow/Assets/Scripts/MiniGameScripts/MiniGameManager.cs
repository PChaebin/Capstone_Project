using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private RecipeManager recipeManager;
    [SerializeField] private RecipeUIManager recipeUIManager;

    private void Start()
    {
        recipeManager.LoadRecipesToJson(); // JSON 데이터 로드

        List<Drinks> randomDrinks = recipeManager.GetRandomRecipes(); // 랜덤 음료 3개 가져오기

        Debug.Log("drinks 개수 : " +  randomDrinks.Count);

        if (randomDrinks != null)
        {
            recipeUIManager.showRecipesUI(randomDrinks); // UI에 표시
        }
    }
}
