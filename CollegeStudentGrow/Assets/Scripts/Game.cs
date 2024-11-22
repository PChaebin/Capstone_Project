using UnityEngine;
using TMPro;

public class GaME : MonoBehaviour
{
    public TMP_Text nameText, dateText, levelText, coinText, stressText;
    public GameObject activityUI, storeUI, activityButton, storeButton;
    public ActivityManager activityManager;

    private void Start()
    {
        UpdateUI();
        activityUI.SetActive(false);
        storeUI.SetActive(false);
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
}
