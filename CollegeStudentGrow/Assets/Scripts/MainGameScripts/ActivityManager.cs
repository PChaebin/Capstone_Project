using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour
{
    public TMP_Text coinText, stressText, dateText, levelText, gradeText, scoreText;
    public GameObject countdownPanel;
    public TMP_Text countdownText;
    public Button[] actionButtons;
    public Button storeButton;

    public GameObject schoolImage;
    public GameObject tripImage;
    public GameObject restImage;
    public GameObject partTimeJobImage;
    public Slider scoreSlider;
    private int activityCount = 0;

    public Image sliderFillImage;

    private void Start()
    {
        countdownPanel.SetActive(false);
        sliderFillImage.sprite = Resources.Load<Sprite>("score_bar_8"); 

        schoolImage.SetActive(false);
        tripImage.SetActive(false);
        restImage.SetActive(false);
        partTimeJobImage.SetActive(false);

        UpdateUI();
    }

    public void GoToSchool()
    {
        ModifyPlayerData(stress: 10, score: 5);
        StartCoroutine(ShowCountdownPanelWithImage(schoolImage, "수업 듣는 중"));
    }

    public void DoPartTimeJob()
    {
        ModifyPlayerData(coin: 20, stress: 10);
        StartCoroutine(ShowCountdownPanelWithImage(partTimeJobImage, "알바하는 중"));
    }

    public void GoOnTrip()
    {
        ModifyPlayerData(coin: -15, stress: -15);
        StartCoroutine(ShowCountdownPanelWithImage(tripImage, "놀러가는 중"));
    }

    public void Rest()
    {
        ModifyPlayerData(stress: -10);
        StartCoroutine(ShowCountdownPanelWithImage(restImage, "휴식하는 중"));
    }


    private void ModifyPlayerData(int coin = 0, int stress = 0, int score = 0)
    {
        var player = DataManager.instance.nowPlayer;

        player.coin = Mathf.Max(0, player.coin + coin);
        player.stress = Mathf.Max(0, player.stress + stress);
        player.score = Mathf.Min(100, player.score + score);

        CompleteActivity();
    }

    private IEnumerator ShowCountdownPanelWithImage(GameObject activityImage, string activityText)
    {
        SetButtonsInteractable(false);
        countdownPanel.SetActive(true);

        activityImage.SetActive(true);

        int countdown = 5;
        while (countdown > 0)
        {
            yield return StartCoroutine(AnimateCountdownText($"{activityText}{new string('.', 3 - countdown % 3)}"));
            countdown--;
        }


        activityImage.SetActive(false);
        countdownPanel.SetActive(false);
        SetButtonsInteractable(true);
    }

    private IEnumerator AnimateCountdownText(string text)
    {
        float fadeDuration = 0.5f;
        float fadeOutTime = 0f;

        countdownText.text = text;
        countdownText.alpha = 0f;

        while (fadeOutTime < fadeDuration)
        {
            fadeOutTime += Time.deltaTime;
            countdownText.alpha = fadeOutTime / fadeDuration;
            yield return null;
        }

        countdownText.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        fadeOutTime = 0f;
        while (fadeOutTime < fadeDuration)
        {
            fadeOutTime += Time.deltaTime;
            countdownText.alpha = 1f - (fadeOutTime / fadeDuration);
            yield return null;
        }

        countdownText.alpha = 0f;
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

        var endingManager = FindObjectOfType<EndingManager>();
        if (endingManager != null)
        {
                endingManager.CheckEnding();
        }
    }

    private void UpdateUI()
    {
        var player = DataManager.instance.nowPlayer;

        player.grade = CalculateGrade(player.score);

        coinText.text = $"Coin: {player.coin}";
        stressText.text = $"Stress: {player.stress}";
        dateText.text = $"{player.date}";
        levelText.text = $"Level: {player.level}";
        gradeText.text = $"Grade: {player.grade}";
        scoreText.text = $"Score: {player.score}";

        scoreSlider.value = player.score;

        Image fillImage = scoreSlider.fillRect.GetComponent<Image>();

        if (player.score >= 99)
            fillImage.color = Color.green; // A+
        else if (player.score >= 87.5)
            fillImage.color = Color.green; // A
        else if (player.score >= 75)
            fillImage.color = Color.green; // B+
        else if (player.score >= 62.5)
            fillImage.color = Color.green; // B
        else if (player.score >= 50)
            fillImage.color = Color.green; // C+
        else if (player.score >= 37.5)
            fillImage.color = Color.green; // C
        else if (player.score >= 25)
            fillImage.color = Color.green; // D+
        else if (player.score >= 12.5)
            fillImage.color = Color.green; // D
        else
            fillImage.color = Color.green; // F
    }


    private string CalculateGrade(int score)
    {
        if (score >= 99) return "A+";
        if (score >= 87.5) return "A";
        if (score >= 75) return "B+";
        if (score >= 62.5) return "B";
        if (score >= 50) return "C+";
        if (score >= 37.5) return "C";
        if (score >= 25) return "D+";
        if (score >= 12.5) return "D";
        return "F";
    }

    public int GetActivityCount()
    {
        return activityCount;
    }

}
