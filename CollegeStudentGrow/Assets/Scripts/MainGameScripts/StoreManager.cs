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
    public GameObject room;  // Room 게임 오브젝트 (구입한 아이템이 포함된 부모)
    public GameObject[] roomItems;  // Room의 각 아이템 (침대, 프레임 등)
    public int[] itemPrices;  // 각 페이지의 아이템 가격

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

        if (roomItems == null || roomItems.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            Debug.LogError("RoomItems 또는 ItemPrices가 비어 있습니다. Inspector에서 값을 추가하세요.");
            return;
        }

        if (DataManager.instance == null || DataManager.instance.nowPlayer == null)
        {
            Debug.LogError("DataManager 또는 nowPlayer가 초기화되지 않았습니다.");
            return;
        }

        maxPage = roomItems.Length; // 최대 페이지 수 설정
        UpdateUI();
        UpdateItemInfo();

        // Room의 모든 아이템 비활성화
        foreach (var item in roomItems)
        {
            item.SetActive(false);
        }
    }

    public void BuyItem()
    {
        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            // 코인 차감 및 UI 업데이트
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log($"아이템 구매 완료! 가격: {cost}");

            // Room 아이템 활성화
            ActivateRoomItem(currentPage);
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
        itemPreview.sprite = roomItems[currentPage].GetComponent<Image>().sprite; // Room 아이템의 스프라이트 표시
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // 페이지 표시 수정 (1부터 시작)
    }

    private void ActivateRoomItem(int index)
    {
        if (index < 0 || index >= roomItems.Length)
        {
            Debug.LogError("잘못된 Room Item 인덱스입니다.");
            return;
        }

        // 선택된 Room 아이템 활성화
        roomItems[index].SetActive(true);
        Debug.Log($"Room Item {index} 활성화됨.");
    }
}
