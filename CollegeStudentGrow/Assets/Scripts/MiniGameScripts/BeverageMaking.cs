using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeverageMaking : MonoBehaviour
{
    [Header("�Ƹ޸�ī�� ��������Ʈ")]
    [SerializeField] List<Sprite> AmericanoSprites = new List<Sprite>();

    [Header("ī��� ��������Ʈ")]
    [SerializeField] List<Sprite> CafeLatteSprites = new List<Sprite>();

    [Header("�ٴҶ�� ��������Ʈ")]
    [SerializeField] List<Sprite> VanillaLatteSprites = new List<Sprite>();

    [Header("�ڸ����̵� ��������Ʈ")]
    [SerializeField] List<Sprite> GrapeFruitAdeSprites = new List<Sprite>();

    [Header("û�������̵� ��������Ʈ")]
    [SerializeField] List<Sprite> GreengrapeAdeSprites = new List<Sprite>();

    [Header("�����̵� ��������Ʈ")]
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
            Debug.Log("recipesQueue �������");
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

            // ���� �� ui ����
            CupUIChange();

            // ������ �Ϸ� Ȯ��
            if (currentStepIndex >= currentDrink.steps.Count)
            {
                Debug.Log($"���� {currentDrink.Name} �ϼ�!");

                // ���� ������ �Ѿ��
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
            Debug.Log("��� ���� ���� �Ϸ�!!");

            // ���� ������ �Ϸ� ���� �� ���� �ܰ�
        }
    }
}
