using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject recipePagePrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText;

    private float recipedisplayTime = 7f;

    public void showRecipesUI(List<Drinks> recipes)
    {
        StartCoroutine(DisplayRecipes());
    }

    public void InitUI(List<Drinks> recipes)
    {
        foreach(var recipe in recipes)
        {
            GameObject newPage = Instantiate(recipePagePrefab, contentParent);
            recipePagePrefab page = newPage.GetComponent<recipePage>();

            page.recipeName.text = recipe.Name;
            page.recipeID.text = recipe.ID;
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
}
