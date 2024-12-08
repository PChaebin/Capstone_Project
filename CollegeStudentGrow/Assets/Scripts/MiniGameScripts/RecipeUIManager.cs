using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.U2D;

public class RecipeUIManager : MonoBehaviour
{
    public static RecipeUIManager Instance { get; private set; }

    [SerializeField] private GameObject recipePagePrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject recipeUI;
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private GameObject bulrPanel;
    [SerializeField] private GameObject currentDrinkImgUI;
    [SerializeField] private GameObject cupUI;

    private float recipedisplayTime = 7f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void showRecipesUI(List<Drinks> drinks)
    {
        InitUI(drinks);
        StartCoroutine(DisplayRecipes());
    }

    /// <summary>
    /// drinks ����Ʈ�� ����� ui ���� �Լ�
    /// </summary>
    /// <param name="drinks"></param>
    public void InitUI(List<Drinks> drinks)
    {
        Debug.Log($"drinks ����Ʈ ����: {drinks.Count}");

        foreach (Transform child in contentParent)
        {
            LeanTween.cancel(child.gameObject);
            Destroy(child.gameObject);
        }

        foreach(var drink in drinks)
        {
            GameObject newPage = Instantiate(recipePagePrefab, contentParent);
            RecipePage page = newPage.GetComponent<RecipePage>();

            page.SetPage(drink);
        }
    }

    /// <summary>
    /// ������ ui Ÿ�̸� �Լ�
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
        countDownText.gameObject.SetActive(false);
        bulrPanel.gameObject.SetActive(false);
        
        // Ÿ�̸� ����
        StartCoroutine(FindObjectOfType<Timer>().StartTimer());

        LeanTween.cancel(recipeUI);
    }

    /// <summary>
    /// �÷��̾ ������ ��ᰡ �����ǿ� ��ġ�� �� �������� �� �̹��� ���� 
    /// </summary>
    public void ChangeCupImg(Sprite newCupSprite)
    {
        if (cupUI == null)
        {
            Debug.LogError("���� ��ġ�� �� ui�� ������������");
        }

        Image cupImage = cupUI.GetComponent<Image>();

        if (cupImage != null)
        {
            cupImage.sprite = newCupSprite;
        }

    }
}
