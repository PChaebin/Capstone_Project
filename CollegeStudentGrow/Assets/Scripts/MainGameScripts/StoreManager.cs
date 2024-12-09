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
            return;
        }

        maxPage = roomItems.Length; // �ִ� ������ �� ����
        UpdateUI();
        UpdateItemInfo();

        for (int i = 0; i < roomItems.Length; i++)
        {
            if (DataManager.instance.nowPlayer.purchasedItems[i])
            {
                roomItems[i].SetActive(true); // ������ ������ 
            }
            else
            {
                roomItems[i].SetActive(false); // �������� ���� ������
            }
        }
    }

    public void BuyItem()
    {
        if (DataManager.instance.nowPlayer.purchasedItems[currentPage]) // �̹� ����
        {
            return; 
        }

        int cost = itemPrices[currentPage];

        if (DataManager.instance.nowPlayer.coin >= cost)
        {         
            DataManager.instance.nowPlayer.coin -= cost;
            DataManager.instance.nowPlayer.purchasedItems[currentPage] = true; // ���� �Ϸ�
            
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
        // ���� �������� ������ ���� ������Ʈ
        itemPriceText.text = $"Price: {itemPrices[currentPage]}";
        itemPreview.sprite = roomItems[currentPage].GetComponent<Image>().sprite; // Room �������� ��������Ʈ ǥ��
        currentPageText.text = $"Page: {currentPage + 1}/{maxPage}"; // ������ ǥ�� ���� (1���� ����)
    }
}
