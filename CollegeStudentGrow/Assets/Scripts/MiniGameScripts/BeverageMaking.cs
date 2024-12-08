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

    [Header("빈 컵 스프라이트")]
    [SerializeField] Sprite emptyCupSprite;

    private Dictionary<string, List<Sprite>> drinkSpriteMap;

    private RecipeManager recipeManager;
    private Queue<Drinks> recipesQueue;

    private Drinks currentDrink;

    private int currentStepIndex = 0;

    private Sprite sprite;

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

    /// <summary>
    /// Dictionary로 음료 - 스프라이트 맵핑
    /// </summary>
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

    /// <summary>
    /// 플레이어가 추가한 재료 확인, 음료 제작 단계
    /// </summary>
    /// <param name="ingredientName"></param>
    public void CheckIngredient(string ingredientName)
    {
        if (currentStepIndex < currentDrink.steps.Count && ingredientName == currentDrink.steps[currentStepIndex])
        {
            currentStepIndex++;

            // 음료 컵 ui 변경
            CupUIChange();

            // 레시피 완료 확인
            if (currentStepIndex == currentDrink.steps.Count)
            {
                Debug.Log($"음료 {currentDrink.Name} 완성!");
                FinalCupImage();

                // 다음 레시피 넘어가기
                NextRecipe();
            }
        }
    }

    /// <summary>
    /// 음료 제작 과정 이미지 변경
    /// </summary>
    private void CupUIChange()
    {
        if (drinkSpriteMap.TryGetValue(currentDrink.Name, out List<Sprite> sprites))
        {
            if(currentStepIndex <= sprites.Count)
            {
                sprite = sprites[currentStepIndex - 1];
                RecipeUIManager.Instance.ChangeCupImg(sprite);
            }
        }
    }

    /// <summary>
    /// 음료 완성 이미지 변경
    /// </summary>
    private void FinalCupImage()
    {
        if (drinkSpriteMap.TryGetValue(currentDrink.Name, out List<Sprite> sprites))
        {
            if (sprites.Count > 0)
            {
                sprite = sprites[currentStepIndex];
                Debug.Log("인덱스 " + (currentStepIndex - 1));
                RecipeUIManager.Instance.ChangeCupImg(sprite);
            }
        }
    }

    /// <summary>
    /// 빈 컵으로 이미지 변경
    /// </summary>
    /// <returns></returns>
    private IEnumerator EmptyCupImage()
    {
        yield return new WaitForSeconds(2f);

        if (emptyCupSprite != null)
        {
            RecipeUIManager.Instance.ChangeCupImg(emptyCupSprite);
        }
    }

    /// <summary>
    /// 다음 레시피로 넘기기
    /// </summary>
    private void NextRecipe()
    {
        if (recipesQueue.Count > 0)
        {
            recipesQueue.Dequeue();
            if(recipesQueue.Count > 0)
            {
                currentDrink = recipesQueue.Peek();
                currentStepIndex = 0;

                StartCoroutine(EmptyCupImage());
            }
        }
        else
        {
            Debug.Log("모든 음료 제작 완료!!");

            // 음료 제작이 완료 됐을 때 다음 단계
        }
    }
}
