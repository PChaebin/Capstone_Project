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
            Debug.LogError("ActivityManager�� Inspector���� �������� �ʾҽ��ϴ�.");
            return;
        }
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding ȣ���: ��¥={player.date}, ����={player.score}, ����={player.grade}");

        // ���� ���� - ���� ����
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

        // ���� ���� - �� ���� ����
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

        // ���� ���� - ��Ʈ���� ���� ����
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

        // ���� ����
        if (player.date >= 30 && player.score >= 60)
        {
            player.endingType = "success";
            DataManager.instance.SaveData();
            LoadEndingScene(2);
            return;
        }

        Debug.Log("���� ���� ������");
    }

    private void LoadEndingScene(int sceneIndex)
    {
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError($"�� {sceneIndex}��(��) ���� ������ �߰����� �ʾҽ��ϴ�.");
        }
    }
}
