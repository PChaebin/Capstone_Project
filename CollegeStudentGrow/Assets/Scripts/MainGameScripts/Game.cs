using UnityEngine;
using TMPro;

public class GaME : MonoBehaviour
{
    public TMP_Text nameText, dateText, levelText, coinText, stressText;
    public GameObject activityUI, storeUI;

    [Header("Room Items")]
    public GameObject[] roomItems; // 구매 가능한 방 아이템 목록

    private void Start()
    {
        UpdateUI();
        activityUI.SetActive(false);
        storeUI.SetActive(false);

        InitializePurchasedItems(); // 구매한 아이템 초기화
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
        // 구매한 아이템을 활성화
        for (int i = 0; i < roomItems.Length; i++)
        {
            if (DataManager.instance.nowPlayer.purchasedItems[i])
            {
                roomItems[i].SetActive(true); // 구매한 아이템 활성화
            }
            else
            {
                roomItems[i].SetActive(false); // 구매하지 않은 아이템 비활성화
            }
        }
    }
}
