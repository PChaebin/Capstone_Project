using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 씬 변경을 위해 추가

public class EndingManager : MonoBehaviour
{
    public TMP_Text endingText; // 엔딩 결과 텍스트 (필요 시 UI용으로 남겨둠)
    public GameObject endingPanel; // 엔딩 UI 패널 (사용하지 않을 수도 있음)
    public ActivityManager activityManager; // ActivityManager 참조

    private PlayerData player;
    private int stressOverThresholdDays = 0; // 스트레스 지속 카운터
    private const int stressThreshold = 90;  // 스트레스 과다 기준
    private int lowCoinDays = 0;

    void Start()
    {
        player = DataManager.instance.nowPlayer; // 현재 플레이어 데이터 가져오기
        if (activityManager == null)
        {
            Debug.LogError("ActivityManager가 Inspector에서 설정되지 않았습니다.");
            return;
        }

        Debug.Log("현재 날짜 (player.date): " + player.date);
        if (endingPanel != null)
        {
            endingPanel.SetActive(false); // 시작 시 엔딩 UI 비활성화
        }
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding 호출됨: 현재 날짜 = {player.date}, 점수 = {player.score}, 학점 = {player.grade}");

        // 1. 실패 엔딩 - 제적 엔딩
        if (player.date >= 30 && player.score < 60)
        {
            int schoolActivities = activityManager.GetActivityCount(); // 학교 가기 활동 횟수 가져오기

            if (schoolActivities < 18)
            {
                Debug.Log("엔딩 조건 충족: 제적 엔딩 (3번 씬으로 이동)");
                LoadEndingScene(2); // 3번 씬으로 로드
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
            Debug.Log("엔딩 조건 충족: 돈 부족 엔딩 (3번 씬으로 이동)");
            LoadEndingScene(2); // 3번 씬으로 로드
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
            Debug.Log("엔딩 조건 충족: 스트레스 과다 엔딩 (3번 씬으로 이동)");
            LoadEndingScene(2); // 3번 씬으로 로드
            return;
        }

        // 4. 성공 또는 평범한 엔딩
        if (player.date >= 30)
        {
            Debug.Log("엔딩 조건 충족: 평범한 엔딩 (3번 씬으로 이동)");
            LoadEndingScene(2); // 3번 씬으로 로드
        }
        else
        {
            Debug.Log("엔딩 조건 미충족");
        }
    }

    private void LoadEndingScene(int sceneIndex)
    {
        Debug.Log($"엔딩 씬 로드: {sceneIndex}번 씬");
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex); // 씬 인덱스를 사용하여 전환
        }
        else
        {
            Debug.LogError($"씬 {sceneIndex}은(는) 빌드 설정에 추가되지 않았습니다.");
        }
    }

    public void RestartGame()
    {
        ResetPlayerData();
        DataManager.instance.SaveData();
        if (endingPanel != null)
        {
            endingPanel.SetActive(false);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("GameStartScene"); // 시작 씬 로드
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