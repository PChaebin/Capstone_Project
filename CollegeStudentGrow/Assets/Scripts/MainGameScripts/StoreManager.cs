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
    public GameObject[] roomItems;  // Room의 각 아이템 (침대, 프레임 등)
    public int[] itemPrices;  // 각 페이지의 아이템 가격

    private int currentPage = 0;   // 현재 페이지 (0부터 시작)
    private int maxPage;

    private void Start()
    {
        if (roomItems == null || roomItems.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            return;
        }

        maxPage = roomItems.Length; // 최대 페이지 수 설정
        UpdateUI();
        UpdateItemInfo();

        for (int i = 0; i < roomItems.Length; i++)
        {
            if (DataManager.instance.nowPlayer.purchasedItems[i])
            {
                roomItems[i].SetActive(true); // 구매한 아이템 
            }
            else
            {
                roomItems[i].SetActive(false); // 구매하지 않은 아이템
            }
        }
    }

    public void BuyItem()
    {
        if (DataManager.instance.nowPlayer.purchasedItems[currentPage]) // 이미 구매
        {
            return; 
        }

        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {         
            DataManager.instance.nowPlayer.coin -= cost;
            DataManager.instance.nowPlayer.purchasedItems[currentPage] = true; // 구매 완료
            
            AudioManager.instance.PlaySfx(AudioManager.SFX.BuyItem);

            roomItems[currentPage].SetActive(true);
            UpdateUI();

            DataManager.instance.SaveData();
        }
    }

    private void UpdateUI()
    {
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
        itemPreview.sprite = roomItems[currentPage].GetComponent<Image>().sprite; // Room 아이템의 스프라이트 표시
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // 페이지 표시 수정 (1부터 시작)
    }
}
