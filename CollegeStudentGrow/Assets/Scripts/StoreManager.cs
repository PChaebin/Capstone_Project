using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text levelText;

    public void BuyItem(int cost)   // ������ ���� �� ȣ��
    {
        if (DataManager.instance.nowPlayer.coin >= cost)
        {
            DataManager.instance.nowPlayer.coin -= cost;
            Debug.Log("������ ���� �Ϸ�");
            UpdateUI();
        }
        else
        {
            Debug.Log("������ �����մϴ�");
        }
    }

    private void UpdateUI()
    {
        coinText.text = "Coin : " + DataManager.instance.nowPlayer.coin.ToString();
        levelText.text = "Level : " + DataManager.instance.nowPlayer.level.ToString();
    }
}
