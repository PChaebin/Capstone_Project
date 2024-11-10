using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text levelText;

    public void BuyItem(int cost)   // 아이템 구매 시 호출
    {
        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log("아이템 구매 완료");
            UpdateUI();
        }
        else
        {
            Debug.Log("동전이 부족합니다");
        }
    }

    private void UpdateUI()
    {
        coinText.text = "Coin : " + DataManager.instance.nowPlayer.coin.ToString();
        levelText.text = "Level : " + DataManager.instance.nowPlayer.level.ToString();
    }
}
