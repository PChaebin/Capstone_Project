using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text coinText;  // ���� ���� ǥ��
    public TMP_Text levelText; // ���� ǥ��
    public TMP_Text itemPriceText; // ������ ���� ǥ��
    public TMP_Text currentPageText; // ���� ������ ǥ��
    public Image itemPreview;  // ������ �̸����� �̹���

    [Header("Game Settings")]
    public GameObject gameScreen;  // ������ ������ ǥ�� ��ġ
    public Sprite[] itemSprites;   // �� �������� ������ �̹���
    public int[] itemPrices;       // �� �������� ������ ����

    private int currentPage = 0;   // ���� ������ (0���� ����)
    private int maxPage;

    private void Start()
    {
        // �ʵ� ��ȿ�� �˻�
        if (coinText == null || levelText == null || itemPriceText == null || currentPageText == null || itemPreview == null)
        {
            Debug.LogError("UI Elements�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� Ȯ���ϼ���.");
            return;
        }

        if (itemSprites == null || itemSprites.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            Debug.LogError("ItemSprites �Ǵ� ItemPrices�� ��� �ֽ��ϴ�. Inspector���� ���� �߰��ϼ���.");
            return;
        }

        if (DataManager.instance == null || DataManager.instance.nowPlayer == null)
        {
            Debug.LogError("DataManager �Ǵ� nowPlayer�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        maxPage = itemSprites.Length; // �ִ� ������ �� ����
        UpdateUI();
        UpdateItemInfo();
    }


    public void BuyItem()
    {
        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            // ���� ���� �� UI ������Ʈ
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log($"������ ���� �Ϸ�! ����: {cost}");

            // ���� ȭ�鿡 ������ ǥ��
            DisplayItemOnGameScreen(itemSprites[currentPage]);
            UpdateUI();
        }
        else
        {
            Debug.Log("������ �����մϴ�.");
        }
    }

    private void UpdateUI()
    {
        // ���ΰ� ���� �ؽ�Ʈ ������Ʈ
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
        // ���� �������� ������ ���� ������Ʈ
        itemPriceText.text = $"Price: {itemPrices[currentPage]}";
        itemPreview.sprite = itemSprites[currentPage];
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // ������ ǥ�� ���� (1���� ����)
    }

    private void DisplayItemOnGameScreen(Sprite itemSprite)
    {
        // ������ �������� ���� ȭ�鿡 ǥ��
        GameObject newItem = new GameObject("PurchasedItem");
        Image itemImage = newItem.AddComponent<Image>();
        itemImage.sprite = itemSprite;

        // ���� ȭ���� �ڽ����� �߰�
        newItem.transform.SetParent(gameScreen.transform, false);
        newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // �߾ӿ� ��ġ
        Debug.Log("������ �������� ���� ȭ�鿡 ǥ�õǾ����ϴ�.");
    }
}
