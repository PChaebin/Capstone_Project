using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text coinText;  // 현재 코인 표시
    public TMP_Text levelText; // 레벨 표시
    public TMP_Text itemPriceText; // 아이템 가격 표시
    public TMP_Text currentPageText; // 현재 페이지 표시
    public Image itemPreview;  // 아이템 미리보기 이미지

    [Header("Game Settings")]
    public GameObject gameScreen;  // 구입한 아이템 표시 위치
    public Sprite[] itemSprites;   // 각 페이지의 아이템 이미지
    public int[] itemPrices;       // 각 페이지의 아이템 가격

    private int currentPage = 0;   // 현재 페이지 (0부터 시작)
    private int maxPage;

    private void Start()
    {
        // 필드 유효성 검사
        if (coinText == null || levelText == null || itemPriceText == null || currentPageText == null || itemPreview == null)
        {
            Debug.LogError("UI Elements가 할당되지 않았습니다. Inspector에서 확인하세요.");
            return;
        }

        if (itemSprites == null || itemSprites.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            Debug.LogError("ItemSprites 또는 ItemPrices가 비어 있습니다. Inspector에서 값을 추가하세요.");
            return;
        }

        if (DataManager.instance == null || DataManager.instance.nowPlayer == null)
        {
            Debug.LogError("DataManager 또는 nowPlayer가 초기화되지 않았습니다.");
            return;
        }

        maxPage = itemSprites.Length; // 최대 페이지 수 설정
        UpdateUI();
        UpdateItemInfo();
    }


    public void BuyItem()
    {
        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            // 코인 차감 및 UI 업데이트
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log($"아이템 구매 완료! 가격: {cost}");

            // 게임 화면에 아이템 표시
            DisplayItemOnGameScreen(itemSprites[currentPage]);
            UpdateUI();
        }
        else
        {
            Debug.Log("동전이 부족합니다.");
        }
    }

    private void UpdateUI()
    {
        // 코인과 레벨 텍스트 업데이트
        coinText.text = $"Coin: {DataManager.instance.nowPlayer.coin}";
        levelText.text = $"Level: {DataManager.instance.nowPlayer.level}";
    }

    public void UpdateCurrentPage(int pageIndex)
    {
        currentPage = pageIndex;
        UpdateItemInfo();
    }

    private void UpdateItemInfo()
    {
        // 현재 페이지의 아이템 정보 업데이트
        itemPriceText.text = $"Price: {itemPrices[currentPage]}";
        itemPreview.sprite = itemSprites[currentPage];
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // 페이지 표시 수정 (1부터 시작)
    }

    private void DisplayItemOnGameScreen(Sprite itemSprite)
    {
        // 구입한 아이템을 게임 화면에 표시
        GameObject newItem = new GameObject("PurchasedItem");
        Image itemImage = newItem.AddComponent<Image>();
        itemImage.sprite = itemSprite;

        // 게임 화면의 자식으로 추가
        newItem.transform.SetParent(gameScreen.transform, false);
        newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // 중앙에 배치
        Debug.Log("구입한 아이템이 게임 화면에 표시되었습니다.");
    }
}
