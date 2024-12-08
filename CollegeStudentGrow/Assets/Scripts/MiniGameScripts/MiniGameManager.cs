using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private RecipeManager recipeManager;
    [SerializeField] private RecipeUIManager recipeUIManager;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject gameUI;

    List<Drinks> randomDrinks;

    private void Awake()
    {
        recipeManager.LoadRecipesToJson(); // JSON 데이터 로드

        randomDrinks = recipeManager.GetRandomRecipes(); // 랜덤 음료 레시피 3개 가져오기

        Debug.Log("drinks 개수 : " + randomDrinks.Count);
    }

    private void Start()
    {
        if(gameUI != null) { gameUI.SetActive(false); }

        if(startButton != null)
        {
            startButton.onClick.AddListener(OnstartButtonClicked);
        }
    }

    /// <summary>
    /// Start 버튼 클릭했을 때 UI 호출함수
    /// </summary>
    private void OnstartButtonClicked()
    {
        if (randomDrinks != null)
        {
            recipeUIManager.showRecipesUI(randomDrinks); // UI에 표시
        }

        startButton.gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
}
