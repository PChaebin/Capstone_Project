using UnityEngine;
using TMPro;

public class GaME : MonoBehaviour
{
    public TMP_Text nameText, dateText, levelText, coinText, stressText;
    public GameObject activityUI, storeUI;

    [Header("Room Items")]
    public GameObject[] roomItems; // ���� ������ �� ������ ���

    private void Start()
    {
        UpdateUI();
        activityUI.SetActive(false);
        storeUI.SetActive(false);

        InitializePurchasedItems(); // ������ ������ �ʱ�ȭ
    }

    public void PerformActivity()
    {
        activityUI.SetActive(!activityUI.activeSelf);
    }

    public void PerformStore()
    {
        storeUI.SetActive(!storeUI.activeSelf);
    }

    public void ExitStore()
    {
        storeUI.SetActive(false);
    }

    public void UpdateUI()
    {
        var player = DataManager.instance.nowPlayer;

        nameText.text = $"Name: {player.name}";
        dateText.text = $"Date: {player.date}";
        levelText.text = $"Level: {player.level}";
        coinText.text = $"Coin: {player.coin}";
        stressText.text = $"Stress: {player.stress}";
    }

    private void InitializePurchasedItems()
    {
        // ������ �������� Ȱ��ȭ
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
}
