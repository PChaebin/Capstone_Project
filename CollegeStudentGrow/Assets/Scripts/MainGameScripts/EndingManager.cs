using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public ActivityManager activityManager;
    private PlayerData player;
    private int stressOverThresholdDays = 0;
    private const int stressThreshold = 90;
    private int lowCoinDays = 0;

    void Start()
    {
        player = DataManager.instance.nowPlayer;

        if (activityManager == null)
        {
            Debug.LogError("ActivityManager가 Inspector에서 설정되지 않았습니다.");
            return;
        }
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding 호출됨: 날짜={player.date}, 점수={player.score}, 학점={player.grade}");

        // 실패 엔딩 - 제적 엔딩
        if (player.date >= 30 && player.score < 60)
        {
            int schoolActivities = activityManager.GetActivityCount();

            if (schoolActivities < 18)
            {
                player.endingType = "expelled";
                DataManager.instance.SaveData();
                LoadEndingScene(2);
                return;
            }
        }

        // 실패 엔딩 - 돈 부족 엔딩
        if (player.coin < 10)
        {
            lowCoinDays++;
        }
        else
        {
            lowCoinDays = 0;
        }

        if (lowCoinDays >= 5)
        {
            player.endingType = "bankrupt";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }

        // 실패 엔딩 - 스트레스 과다 엔딩
        if (player.stress >= stressThreshold)
        {
            stressOverThresholdDays++;
        }
        else
        {
            stressOverThresholdDays = 0;
        }

        if (stressOverThresholdDays >= 5)
        {
            player.endingType = "stress";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }

        // 성공 엔딩
        if (player.date >= 30 && player.score >= 60)
        {
            player.endingType = "success";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }

        Debug.Log("엔딩 조건 미충족");
    }

    private void LoadEndingScene(int sceneIndex)
    {
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"씬 {sceneIndex}은(는) 빌드 설정에 추가되지 않았습니다.");
        }
    }
}
