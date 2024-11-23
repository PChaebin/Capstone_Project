using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text dateText;
    public TMP_Text levelText;
    public TMP_Text stressText;
    public TMP_Text scoreText;
    public TMP_Text gradeText;

    public TMP_Text endingText; // 엔딩 결과 텍스트
    public GameObject endingPanel; // 엔딩 UI 패널
    private PlayerData player;

    void Start()
    {
        player = DataManager.instance.nowPlayer; // 현재 플레이어 데이터 가져오기
        if (endingPanel == null || endingText == null)
        {
            Debug.LogError("EndingPanel 또는 EndingText가 Inspector에서 설정되지 않았습니다.");
            return;
        }
        Debug.Log("현재 날짜 (player.date): " + player.date);
        endingPanel.SetActive(false); // 시작 시 엔딩 UI 비활성화
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding 호출됨: 현재 날짜 = {player.date}, 점수 = {player.score}, 학점 = {player.grade}");

        if (player.date >= 30) // 날짜 조건 충족
        {
            string title = "";
            string message = "";

            if (player.grade == "A+" && player.score >= 95) // 조건 불충족
            {
                title = "대기업 취직 성공";
                message = "축하합니다! A+ 성적으로 대기업에 취직했습니다!";
            }
            else if (player.grade == "B" || player.grade == "B+") // 조건 불충족
            {
                title = "중소기업 취직 성공";
                message = "보통 성적으로 중소기업에 취직했습니다!";
            }
            else // 평범한 인생 메시지
            {
                title = "평범한 인생";
                message = "평범한 삶을 살았습니다.";
            }

            ShowEnding(title, message); // 이 코드가 실행될 가능성이 높음
        }
        else
        {
            Debug.Log("엔딩 조건 미충족");
        }

    }


    private void ShowEnding(string title, string message)
    {
        endingPanel.SetActive(true);
        endingText.text = $"[{title}]\n\n{message}";
        Time.timeScale = 0; // 게임 정지
    }

    public void RestartGame()
    {
        ResetPlayerData();
        DataManager.instance.SaveData();
        endingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit(); // 실제 게임 빌드에서 작동
    }

    private void ResetPlayerData()
    {
        player.coin = 10;
        player.date = 1;
        player.level = 1;
        player.stress = 0;
        player.score = 0;
        player.grade = "F";

        UpdateUI();
        Debug.Log("플레이어 데이터 초기화됨");
    }

    private void UpdateUI()
    {
        coinText.text = "코인: " + player.coin.ToString();
        dateText.text = "날짜: " + player.date.ToString();
        levelText.text = "레벨: " + player.level.ToString();
        stressText.text = "스트레스: " + player.stress.ToString();
        scoreText.text = "점수: " + player.score.ToString();
        gradeText.text = "학점: " + player.grade;
    }
}
