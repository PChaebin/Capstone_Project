using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject recipePagePrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private GameObject bulrPanel;

    private float recipedisplayTime = 7f;

    public void showRecipesUI(List<Drinks> drinks)
    {
        InitUI(drinks);
        StartCoroutine(DisplayRecipes());
    }

    /// <summary>
    /// drinks ����Ʈ�� ����� ui ���� �Լ�
    /// </summary>
    /// <param name="drinks"></param>
    public void InitUI(List<Drinks> drinks)
    {
        Debug.Log($"drinks ����Ʈ ����: {drinks.Count}");

        foreach (Transform child in contentParent)
        {
            LeanTween.cancel(child.gameObject);
            Destroy(child.gameObject);
        }

        foreach(var drink in drinks)
        {
            GameObject newPage = Instantiate(recipePagePrefab, contentParent);
            RecipePage page = newPage.GetComponent<RecipePage>();

            page.SetPage(drink);
        }
    }

    /// <summary>
    /// ������ ui Ÿ�̸� �Լ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayRecipes()
    {
        float countdownStartTime = 3;
        float remainingTime = recipedisplayTime;

        while (remainingTime > 0)
        {
            if (remainingTime <= countdownStartTime)
            {
                countDownText.text = Mathf.CeilToInt(remainingTime).ToString();
                countDownText.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;

            if (remainingTime <= 0)
            {
                break;
            }
        }

        recipeUI.SetActive(false);
        countDownText.gameObject.SetActive(false);
        bulrPanel.gameObject.SetActive(false);
        
        // Ÿ�̸� ����
        StartCoroutine(FindObjectOfType<Timer>().StartTimer());

        LeanTween.cancel(recipeUI);
    }
}
