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
  
    }

    public void CheckEnding()
    {
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
        if (player.date >= 30 && player.score >= 90)
        {
            player.endingType = "success";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }
        //노말 엔딩
        if (player.date >= 30 && player.score >= 60)
        {
            player.endingType = "normal";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }

    }

    private void LoadEndingScene(int sceneIndex)
    {
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
