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
        recipeManager.LoadRecipesToJson(); // JSON ������ �ε�

        randomDrinks = recipeManager.GetRandomRecipes(); // ���� ���� ������ 3�� ��������

        Debug.Log("drinks ���� : " + randomDrinks.Count);
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
    /// Start ��ư Ŭ������ �� UI ȣ���Լ�
    /// </summary>
    private void OnstartButtonClicked()
    {
        if (randomDrinks != null)
        {
            recipeUIManager.showRecipesUI(randomDrinks); // UI�� ǥ��
        }

        startButton.gameObject.SetActive(false);
        gameUI.SetActive(true);
    }
}
