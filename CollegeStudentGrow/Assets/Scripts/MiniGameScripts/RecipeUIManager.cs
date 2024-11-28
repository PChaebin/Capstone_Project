using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText;

    private float recipedisplayTime = 7f;

    public void showRecipesUI(List<Drinks> recipes)
    {
        StartCoroutine(DisplayRecipes());
    }

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
