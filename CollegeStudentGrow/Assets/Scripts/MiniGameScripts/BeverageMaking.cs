using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeverageMaking : MonoBehaviour
{
    [Header("아메리카노 스프라이트")]
    [SerializeField] List<Sprite> AmericanoSprites = new List<Sprite>();

    [Header("카페라떼 스프라이트")]
    [SerializeField] List<Sprite> CafeLatteSprites = new List<Sprite>();

    [Header("바닐라라떼 스프라이트")]
    [SerializeField] List<Sprite> VanillaLatteSprites = new List<Sprite>();

    [Header("자몽에이드 스프라이트")]
    [SerializeField] List<Sprite> GrapeFruitAdeSprites = new List<Sprite>();

    [Header("청포도에이드 스프라이트")]
    [SerializeField] List<Sprite> GreengrapeAdeSprites = new List<Sprite>();

    [Header("레몬에이드 스프라이트")]
    [SerializeField] List<Sprite> LemonAdeSprites = new List<Sprite>();

    private Dictionary<string, List<Sprite>> drinkSpriteMap;

    private RecipeManager recipeManager;
    private Queue<Drinks> recipesQueue;

    private Drinks currentDrink;

    private int currentStepIndex = 0;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
        recipesQueue = recipeManager.GetRecipeQueue();

        if(recipesQueue.Count > 0)
        {
            currentDrink = recipesQueue.Peek();
        }
        else
        {
            Debug.Log("recipesQueue 비어있음");
        }

        InitDrinkSpriteMap();
    }

    private void InitDrinkSpriteMap()
    {
        drinkSpriteMap = new Dictionary<string, List<Sprite>>
        {
            {"Americano", AmericanoSprites },
            {"CafeLatte" ,CafeLatteSprites},
            {"VanillaLatte", VanillaLatteSprites },
            {"GrapeFruitAde", GrapeFruitAdeSprites },
            {"GreengrapeAde", GreengrapeAdeSprites },
            {"LemonAde", LemonAdeSprites }
        };
    }

    public void CheckIngredient(string ingredientName)
    {
        if (ingredientName == currentDrink.steps[currentStepIndex])
        {
            currentStepIndex++;

            // 음료 컵 ui 변경
            CupUIChange();

            // 레시피 완료 확인
            if (currentStepIndex >= currentDrink.steps.Count)
            {
                Debug.Log($"음료 {currentDrink.Name} 완성!");

                // 다음 레시피 넘어가기
                NextRcipe();
            }
        }
    }

    private void CupUIChange()
    {
        Sprite sprite;

        if (drinkSpriteMap.TryGetValue(currentDrink.Name, out List<Sprite> sprites))
        {
            if(currentStepIndex - 1 < sprites.Count)
            {
                sprite = sprites[currentStepIndex - 1];
                RecipeUIManager.Instance.ChangeCupImg(sprite);
            }
        }
    }

    private void NextRcipe()
    {
        if (recipesQueue.Count > 0)
        {
            recipesQueue.Dequeue();
            currentDrink = recipesQueue.Peek();
            currentStepIndex = 0;
        }
        else
        {
            Debug.Log("모든 음료 제작 완료!!");

            // 음료 제작이 완료 됐을 때 다음 단계
        }
    }
}
