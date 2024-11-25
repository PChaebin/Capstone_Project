using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public TMP_Text endingText; // 엔딩 결과 텍스트
    public GameObject endingPanel; // 엔딩 UI 패널
    public ActivityManager activityManager; // ActivityManager 참조

    private PlayerData player;
    private int stressOverThresholdDays = 0; // 스트레스 지속 카운터
    private const int stressThreshold = 90;  // 스트레스 과다 기준
    private int lowCoinDays = 0;
    void Start()
    {
        player = DataManager.instance.nowPlayer; // 현재 플레이어 데이터 가져오기
        if (endingPanel == null || endingText == null)
        {
            Debug.LogError("EndingPanel 또는 EndingText가 Inspector에서 설정되지 않았습니다.");
            return;
        }

        if (activityManager == null)
        {
            Debug.LogError("ActivityManager가 Inspector에서 설정되지 않았습니다.");
            return;
        }

        Debug.Log("현재 날짜 (player.date): " + player.date);
        endingPanel.SetActive(false); // 시작 시 엔딩 UI 비활성화
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding 호출됨: 현재 날짜 = {player.date}, 점수 = {player.score}, 학점 = {player.grade}");

        string title = "";
        string message = "";

        // 1. 실패 엔딩 - 제적 엔딩
        if (player.date >= 30 && player.score < 60)
        {
            int schoolActivities = activityManager.GetActivityCount(); // 학교 가기 활동 횟수 가져오기

            if (schoolActivities < 18)
            {
                title = "제적 엔딩";
                message = "나: 아 오늘 학교 가기 귀찮은데... 가지 말까?\n\n(눈 깜박)\n\n(눈 앞에 들이밀어진 제적 통지서)\n\n엄마: 야 너 이거 뭐야?";
                ShowEnding(title, message);
                return;
            }
        }

        // 2. 실패 엔딩 - 돈 부족 엔딩
        if (player.coin < 10)
        {
            lowCoinDays++; // 돈이 부족한 날 카운트 증가
        }
        else
        {
            lowCoinDays = 0; // 돈이 10원 이상이면 카운터 초기화
        }

        if (lowCoinDays >= 5)
        {
            title = "자취방 쫓겨나 귀가 엔딩";
            message = "집주인: 304호 월세가 안 들어왔습니다. 집 빼주세요.\n\n(특.이.허네)";
            ShowEnding(title, message);
            return;
        }

        // 3. 실패 엔딩 - 스트레스 과다 엔딩
        if (player.stress >= stressThreshold)
        {
            stressOverThresholdDays++;
        }
        else
        {
            stressOverThresholdDays = 0; // 스트레스가 기준치 미만이면 카운터 초기화
        }

        if (stressOverThresholdDays >= 5)
        {
            title = "응급실 행 엔딩";
            message = "간호사: 환자분 정신이 드세요?\n\n의사: 스트레스 과다로 인한 홧병입니다. 치료가 필요합니다.";
            ShowEnding(title, message);
            return;
        }

        // 4. 성공 또는 평범한 엔딩
        if (player.date >= 30)
        {
            if (player.grade == "A+" && player.score >= 95)
            {
                title = "대기업 취직 성공";
                message = "축하합니다! A+ 성적으로 대기업에 취직했습니다!";
            }
            else if (player.grade == "B" || player.grade == "B+")
            {
                title = "중소기업 취직 성공";
                message = "보통 성적으로 중소기업에 취직했습니다!";
            }
            else
            {
                title = "평범한 인생";
                message = "평범한 삶을 살았습니다.";
            }

            ShowEnding(title, message);
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
        // 필요한 UI를 업데이트 (필드 추가 가능)
    }
}
