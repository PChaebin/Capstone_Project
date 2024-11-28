using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Json 데이터 클래스
/// </summary>
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

public class RecipeManager : MonoBehaviour
{
    private DrinksList drinks;
    private int recipeCount = 3;

    /// <summary>
    /// Json 데이터 로드 함수
    /// </summary>
    private void LoadRecipesToJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CafeRecipe");

        if (jsonFile != null)
        {
            drinks = JsonUtility.FromJson<DrinksList>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Json 파일 로드 실패!!");
        }
    }

    /// <summary>
    /// 랜덤 레시피 선택 함수
    /// </summary>
    /// <returns></returns>
    public List<Drinks> GetRandomRecipes()
    {
        if(drinks == null || drinks.drinksList.Count == 0)
        {
            Debug.LogError("레시피 Json 파일이 존재하지않음!!");
            return null;
        }

        List<Drinks> selectedRecipes = new List<Drinks>();
        List<Drinks> usedIndexes = new List<int>();

        for(int i = 0; i < recipeCount; i++)
        {
            int randomIndex = Random.Range(0, drinks.drinksList.Count);

            if(!usedIndexes.Contains(randomIndex))
            {
                usedIndexes.Add(randomIndex);
                selectedRecipes.Add(drinks.drinksList[randomIndex]);
            }
        }

        return selectedRecipes;
    }
}
