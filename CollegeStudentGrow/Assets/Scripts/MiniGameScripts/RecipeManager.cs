using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Json ������ Ŭ����
/// </summary>
[System.Serializable]
public class Drinks
{
    public string Name;
    public string FinalImageFile;
    public string FinalImage;
    public List<string> Recipes;
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

    private Queue<Drinks> recipesQueue = new Queue<Drinks>();

    /// <summary>
    /// Json ������ �ε� �Լ�
    /// </summary>
    public void LoadRecipesToJson()
    {
        recipesQueue.Clear();

        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CafeRecipe");
        Debug.Log("json ���� ����");

        if (jsonFile != null)
        {
            Debug.Log("JSON ���� ����: " + jsonFile.text);
            drinks = JsonUtility.FromJson<DrinksList>(jsonFile.text);
            Debug.Log("json ���� ����2");

            if (drinks != null && drinks.drinksList.Count > 0)
            {
                Debug.Log($"�ε�� ������ ����: {drinks.drinksList.Count}");
            }
            else
            {
                Debug.LogError("JSON �Ľ� ���� �Ǵ� �����Ͱ� ��� �ֽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Json ���� �ε� ����!!");
        }
    }

    /// <summary>
    /// ���� ������ ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public List<Drinks> GetRandomRecipes()
    {
        // Queue<Drinks> recipes = new Queue<Drinks>();

        if(drinks == null || drinks.drinksList.Count == 0)
        {
            Debug.LogError("������ Json ������ ������������!!");
            return null;
        }

        List<Drinks> selectedRecipes = new List<Drinks>();
        List<int> usedIndexes = new List<int>();

        while(selectedRecipes.Count < recipeCount)
        {
            int randomIndex = Random.Range(0, drinks.drinksList.Count);

            if (!usedIndexes.Contains(randomIndex))
            {
                usedIndexes.Add(randomIndex);
                selectedRecipes.Add(drinks.drinksList[randomIndex]);
                recipesQueue.Enqueue(drinks.drinksList[randomIndex]);
            }
        }

        return selectedRecipes;
    }

    public Queue<Drinks> GetRecipeQueue()
    {
        return recipesQueue;
    }
}