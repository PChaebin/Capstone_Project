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
    public GameObject room;  // Room ���� ������Ʈ (������ �������� ���Ե� �θ�)
    public GameObject[] roomItems;  // Room�� �� ������ (ħ��, ������ ��)
    public int[] itemPrices;  // �� �������� ������ ����

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

        if (roomItems == null || roomItems.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            Debug.LogError("RoomItems �Ǵ� ItemPrices�� ��� �ֽ��ϴ�. Inspector���� ���� �߰��ϼ���.");
            return;
        }

        if (DataManager.instance == null || DataManager.instance.nowPlayer == null)
        {
            Debug.LogError("DataManager �Ǵ� nowPlayer�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        maxPage = roomItems.Length; // �ִ� ������ �� ����
        UpdateUI();
        UpdateItemInfo();

        // Room�� ��� ������ ��Ȱ��ȭ
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
            // ���� ���� �� UI ������Ʈ
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log($"������ ���� �Ϸ�! ����: {cost}");

            // Room ������ Ȱ��ȭ
            ActivateRoomItem(currentPage);
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
        itemPreview.sprite = roomItems[currentPage].GetComponent<Image>().sprite; // Room �������� ��������Ʈ ǥ��
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // ������ ǥ�� ���� (1���� ����)
    }

    private void ActivateRoomItem(int index)
    {
        if (index < 0 || index >= roomItems.Length)
        {
            Debug.LogError("�߸��� Room Item �ε����Դϴ�.");
            return;
        }

        // ���õ� Room ������ Ȱ��ȭ
        roomItems[index].SetActive(true);
        Debug.Log($"Room Item {index} Ȱ��ȭ��.");
    }
}
