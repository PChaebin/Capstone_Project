using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private DrinksList drinks;

    private float recipedisplayTime = 30f;

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

    public void gmaeStatus()
    {
        startButton.gameObject.SetActive(false);

        recipeUI.SetActive(true);

        StartCoroutine(DisplayRecipes());
    }

    private IEnumerator DisplayRecipes()
    {
        yield return new WaitForSeconds(recipedisplayTime);

        recipeUI.SetActive(false);
    }
}
