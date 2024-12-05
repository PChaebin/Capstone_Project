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

    [Header("�� �� ��������Ʈ")]
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
            Debug.Log("recipesQueue �������");
        }

        InitDrinkSpriteMap();
    }

    /// <summary>
    /// Dictionary�� ���� - ��������Ʈ ����
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
    /// �÷��̾ �߰��� ��� Ȯ��, ���� ���� �ܰ�
    /// </summary>
    /// <param name="ingredientName"></param>
    public void CheckIngredient(string ingredientName)
    {
        if (currentStepIndex < currentDrink.steps.Count && ingredientName == currentDrink.steps[currentStepIndex])
        {
            currentStepIndex++;

            // ���� �� ui ����
            CupUIChange();

            // ������ �Ϸ� Ȯ��
            if (currentStepIndex == currentDrink.steps.Count)
            {
                Debug.Log($"���� {currentDrink.Name} �ϼ�!");
                FinalCupImage();

                // ���� ������ �Ѿ��
                NextRecipe();
            }
        }
    }

    /// <summary>
    /// ���� ���� ���� �̹��� ����
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
    /// ���� �ϼ� �̹��� ����
    /// </summary>
    private void FinalCupImage()
    {
        if (drinkSpriteMap.TryGetValue(currentDrink.Name, out List<Sprite> sprites))
        {
            if (sprites.Count > 0)
            {
                sprite = sprites[currentStepIndex];
                Debug.Log("�ε��� " + (currentStepIndex - 1));
                RecipeUIManager.Instance.ChangeCupImg(sprite);
            }
        }
    }

    /// <summary>
    /// �� ������ �̹��� ����
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
    /// ���� �����Ƿ� �ѱ��
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
            Debug.Log("��� ���� ���� �Ϸ�!!");

            // ���� ������ �Ϸ� ���� �� ���� �ܰ�
        }
    }
}
