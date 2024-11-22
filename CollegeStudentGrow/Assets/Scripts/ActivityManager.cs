using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ActivityManager : MonoBehaviour
{
    public TMP_Text coinText, stressText, dateText, levelText, gradeText, scoreText;
    public GameObject countdownPanel;
    public TMP_Text countdownText;
    public Button[] actionButtons;
    public Button storeButton;

    private int activityCount = 0;

    private void Start()
    {
        countdownPanel.SetActive(false);
        UpdateUI();
    }

    public void GoToSchool()
    {
        Debug.Log("ÇĞ±³ °¡±â ¼±ÅÃµÊ");
        ModifyPlayerData(stress: 5, score: 5);
        StartCoroutine(ShowCountdownPanel());
    }

    public void DoPartTimeJob()
    {
        Debug.Log("¾Ë¹ÙÇÏ±â ¼±ÅÃµÊ");
        ModifyPlayerData(coin: 20, stress: 3);
        StartCoroutine(ShowCountdownPanel());
    }

    public void GoOnTrip()
    {
        Debug.Log("³î·¯°¡±â ¼±ÅÃµÊ");
        ModifyPlayerData(coin: -15, stress: -5);
        StartCoroutine(ShowCountdownPanel());
    }

    public void Rest()
    {
        Debug.Log("ÈŞ½ÄÇÏ±â ¼±ÅÃµÊ");
        ModifyPlayerData(stress: -10);
        StartCoroutine(ShowCountdownPanel());
    }

    private void ModifyPlayerData(int coin = 0, int stress = 0, int score = 0)
    {
        var player = DataManager.instance.nowPlayer;

        player.coin = Mathf.Max(0, player.coin + coin);
        player.stress = Mathf.Max(0, player.stress + stress);
        player.score += score;

        CompleteActivity();
    }

    private IEnumerator ShowCountdownPanel()
    {
        SetButtonsInteractable(false);
        countdownPanel.SetActive(true);

        int countdown = 5;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }

        countdownPanel.SetActive(false);
        SetButtonsInteractable(true);
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in actionButtons)
        {
            button.interactable = interactable;
        }
        storeButton.interactable = interactable;
    }

    private void CompleteActivity()
    {
        activityCount++;
        if (activityCount >= 2)
        {
            DateUp();
            activityCount = 0;
        }

        UpdateUI();
        DataManager.instance.SaveData();
    }

    private void DateUp()
    {
        DataManager.instance.nowPlayer.date++;
        Debug.Log("³¯Â¥°¡ Áõ°¡Çß½À´Ï´Ù.");
    }

    private void UpdateUI()
    {
        var player = DataManager.instance.nowPlayer;

        player.grade = CalculateGrade(player.score);

        coinText.text = $"Coin: {player.coin}";
        stressText.text = $"Stress: {player.stress}";
        dateText.text = $"Date: {player.date}";
        levelText.text = $"Level: {player.level}";
        gradeText.text = $"Grade: {player.grade}";
        scoreText.text = $"Score: {player.score}";
    }

    private string CalculateGrade(int score)
    {
        if (score >= 95) return "A+";
        if (score >= 85) return "A";
        if (score >= 75) return "B+";
        if (score >= 65) return "B";
        if (score >= 55) return "C+";
        if (score >= 45) return "C";
        if (score >= 35) return "D+";
        if (score >= 25) return "D";
        return "F";
    }
}
