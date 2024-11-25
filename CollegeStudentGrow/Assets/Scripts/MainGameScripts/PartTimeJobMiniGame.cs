using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartTimeJobMiniGame : MonoBehaviour
{
    [System.Serializable]
    public class Drinks
    {
        public string Name;
        public string FinalImage;
        public List<string> steps;
        public string Description;
    }

    [System.Serializable]
    public class DrinksList
    {
        public List<Drinks> drinksList;
    }

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText; 

    private DrinksList drinks;

    private float recipedisplayTime = 7f;

    private void Start()
    {
        LoadRecipesToJson();
    }

    private void LoadRecipesToJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CafeRecipe");

        if(jsonFile != null)
        {
            drinks = JsonUtility.FromJson<DrinksList>(jsonFile.text);
        }
    }

    public void gameStatus()
    {
        startButton.gameObject.SetActive(false);
        recipeUI.SetActive(true);

        StartCoroutine(DisplayRecipes());
    }

    private IEnumerator DisplayRecipes()
    {
        float countdownStartTime = 3;
        float remainingTime = recipedisplayTime;

        while(remainingTime > 0)
        {
            if(remainingTime <= countdownStartTime)
            {
                countDownText.text = Mathf.CeilToInt(remainingTime).ToString();
                countDownText.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1f);
            remainingTime--;

            if(remainingTime <= 0)
            {
                break;
            }
        }

        recipeUI.SetActive(false);
    }
}
