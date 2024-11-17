using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ActivityManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text stressText;
    public TMP_Text dateText;
    public TMP_Text levelText;
    public GameObject countdownPanel; // 카운트다운 창
    public TMP_Text countdownText;    // 카운트다운 텍스트
    public Button[] actionButtons;    // 활동 버튼들
    public Button storeButton;        // 상점 버튼

    public TMP_Text gradeText; // 학점 UI 텍스트
    public TMP_Text scoreText;

    private int activityCount = 0;

    private void Start()
    {
        countdownPanel.SetActive(false); // 시작 시 비활성화
    }

    public void GoToSchool()
    {
        Debug.Log("학교 가기 선택됨");
        DataManager.instance.nowPlayer.stress += 5;  // 스트레스 증가
        DataManager.instance.nowPlayer.score += 5;
        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    private IEnumerator ShowCountdownPanel()
    {
        // 모든 버튼 비활성화
        SetButtonsInteractable(false);

        countdownPanel.SetActive(true); // 카운트다운 활성화
        int countdown = 5;

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // 카운트다운 업데이트
            yield return new WaitForSeconds(1);        // 1초 대기
            countdown--;
        }

        countdownPanel.SetActive(false); // 카운트다운 비활성화
        SetButtonsInteractable(true);    // 버튼 다시 활성화
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in actionButtons)
        {
            button.interactable = interactable; // 활동 버튼 상호작용 설정
        }

        // 상점 버튼도 비활성화/활성화
        if (storeButton != null)
        {
            storeButton.interactable = interactable;
        }
    }

    public void DoPartTimeJob()
    {
        Debug.Log("알바하기 선택됨");
        DataManager.instance.nowPlayer.coin += 20;
        DataManager.instance.nowPlayer.stress += 3;
        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    public void GoOnTrip()
    {
        Debug.Log("놀러가기 선택됨");
        DataManager.instance.nowPlayer.coin -= 15;
        DataManager.instance.nowPlayer.stress -= 5;

        DataManager.instance.nowPlayer.coin = Mathf.Max(DataManager.instance.nowPlayer.coin, 0);
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    public void Rest()
    {
        Debug.Log("휴식하기 선택됨");
        DataManager.instance.nowPlayer.stress -= 10;

        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
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
    }

    private void DateUp()
    {
        DataManager.instance.nowPlayer.date++;
        dateText.text = "Date : " + DataManager.instance.nowPlayer.date.ToString();
    }

    private void UpdateUI()
    {
        PlayerData player = DataManager.instance.nowPlayer;

        // 학점 계산
        player.grade = CalculateGrade(player.score);

        // UI 업데이트
        coinText.text = "Coin : " + player.coin.ToString();
        stressText.text = "Stress : " + player.stress.ToString();
        dateText.text = "Date : " + player.date.ToString();
        levelText.text = "Level : " + player.level.ToString();
        gradeText.text = "Grade : " + player.grade;
        scoreText.text = "Score : " + player.score.ToString();
    }

    private string CalculateGrade(int score)
    {
        if (score >= 95) return "A+";
        else if (score >= 90) return "A";
        else if (score >= 85) return "B+";
        else if (score >= 80) return "B";
        else if (score >= 75) return "C+";
        else if (score >= 70) return "C";
        else if (score >= 65) return "D+";
        else if (score >= 60) return "D";
        else return "F";
    }
}
