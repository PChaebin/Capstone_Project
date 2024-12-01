using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject recipePagePrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText;

    private float recipedisplayTime = 7f;

    public void showRecipesUI(List<Drinks> drinks)
    {
        InitUI(drinks);
        StartCoroutine(DisplayRecipes());
    }

    public void InitUI(List<Drinks> drinks)
    {
        Debug.Log($"drinks 리스트 길이: {drinks.Count}");

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
    /// 레시피 ui 타이머 함수
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
    }

    private void OnDestroy()
    {
        LeanTween.reset(); // 모든 LeanTween 애니메이션 초기화
    }

}
