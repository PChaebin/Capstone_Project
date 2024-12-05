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
    /// drinks 리스트를 사용한 ui 세팅 함수
    /// </summary>
    /// <param name="drinks"></param>
    public void InitUI(List<Drinks> drinks)
    {
        Debug.Log($"drinks 리스트 길이: {drinks.Count}");

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
    /// 레시피 ui 타이머 함수
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
        
        // 타이머 시작
        StartCoroutine(FindObjectOfType<Timer>().StartTimer());

        LeanTween.cancel(recipeUI);
    }

    /// <summary>
    /// 플레이어가 선택한 재료가 레시피와 일치할 때 제작중인 컵 이미지 변경 
    /// </summary>
    public void ChangeCupImg(Sprite newCupSprite)
    {
        if (cupUI == null)
        {
            Debug.LogError("씬에 배치된 컵 ui가 설정되지않음");
        }

        Image cupImage = cupUI.GetComponent<Image>();

        if (cupImage != null)
        {
            cupImage.sprite = newCupSprite;
        }

    }
}
