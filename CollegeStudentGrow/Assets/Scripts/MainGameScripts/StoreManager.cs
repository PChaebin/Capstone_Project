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
    public GameObject[] roomItems;  // Room�� �� ������ (ħ��, ������ ��)
    public int[] itemPrices;  // �� �������� ������ ����

    private int currentPage = 0;   // ���� ������ (0���� ����)
    private int maxPage;

    private void Start()
    {
        if (roomItems == null || roomItems.Length == 0 || itemPrices == null || itemPrices.Length == 0)
        {
            Debug.LogError("RoomItems �Ǵ� ItemPrices�� ��� �ֽ��ϴ�. Inspector���� ���� �߰��ϼ���.");
            return;
        }

        maxPage = roomItems.Length; // �ִ� ������ �� ����
        UpdateUI();
        UpdateItemInfo();

        // ������ ������ �ε� �� Ȱ��ȭ
        for (int i = 0; i < roomItems.Length; i++)
        {
            if (DataManager.instance.nowPlayer.purchasedItems[i])
            {
                roomItems[i].SetActive(true); // ������ ������ Ȱ��ȭ
            }
            else
            {
                roomItems[i].SetActive(false); // �������� ���� ������ ��Ȱ��ȭ
            }
        }
    }

    public void BuyItem()
    {
        // �̹� ������ ���������� Ȯ��
        if (DataManager.instance.nowPlayer.purchasedItems[currentPage])
        {
            Debug.Log("�̹� ������ �������Դϴ�.");
            return; // �Լ� ����
        }

        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            // ���� ���� �� ���� ������Ʈ
            DataManager.instance.nowPlayer.coin -= cost;
            DataManager.instance.nowPlayer.purchasedItems[currentPage] = true; // ���� ���� ����
            Debug.Log($"������ ���� �Ϸ�! ����: {cost}");

            // Room ������ Ȱ��ȭ
            roomItems[currentPage].SetActive(true);
            UpdateUI();

            // ������ ����
            DataManager.instance.SaveData();
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
}
