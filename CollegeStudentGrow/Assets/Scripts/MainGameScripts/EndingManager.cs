using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�

public class EndingManager : MonoBehaviour
{
    public TMP_Text endingText; // ���� ��� �ؽ�Ʈ (�ʿ� �� UI������ ���ܵ�)
    public GameObject endingPanel; // ���� UI �г� (������� ���� ���� ����)
    public ActivityManager activityManager; // ActivityManager ����

    private PlayerData player;
    private int stressOverThresholdDays = 0; // ��Ʈ���� ���� ī����
    private const int stressThreshold = 90;  // ��Ʈ���� ���� ����
    private int lowCoinDays = 0;

    void Start()
    {
        player = DataManager.instance.nowPlayer; // ���� �÷��̾� ������ ��������
        if (activityManager == null)
        {
            Debug.LogError("ActivityManager�� Inspector���� �������� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log("���� ��¥ (player.date): " + player.date);
        if (endingPanel != null)
        {
            endingPanel.SetActive(false); // ���� �� ���� UI ��Ȱ��ȭ
        }
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding ȣ���: ���� ��¥ = {player.date}, ���� = {player.score}, ���� = {player.grade}");

        // 1. ���� ���� - ���� ����
        if (player.date >= 30 && player.score < 60)
        {
            int schoolActivities = activityManager.GetActivityCount(); // �б� ���� Ȱ�� Ƚ�� ��������

            if (schoolActivities < 18)
            {
                Debug.Log("���� ���� ����: ���� ���� (3�� ������ �̵�)");
                LoadEndingScene(2); // 3�� ������ �ε�
                return;
            }
        }

        // 2. ���� ���� - �� ���� ����
        if (player.coin < 10)
        {
            lowCoinDays++; // ���� ������ �� ī��Ʈ ����
        }
        else
        {
            lowCoinDays = 0; // ���� 10�� �̻��̸� ī���� �ʱ�ȭ
        }

        if (lowCoinDays >= 5)
        {
            Debug.Log("���� ���� ����: �� ���� ���� (3�� ������ �̵�)");
            LoadEndingScene(2); // 3�� ������ �ε�
            return;
        }

        // 3. ���� ���� - ��Ʈ���� ���� ����
        if (player.stress >= stressThreshold)
        {
            stressOverThresholdDays++;
        }
        else
        {
            stressOverThresholdDays = 0; // ��Ʈ������ ����ġ �̸��̸� ī���� �ʱ�ȭ
        }

        if (stressOverThresholdDays >= 5)
        {
            Debug.Log("���� ���� ����: ��Ʈ���� ���� ���� (3�� ������ �̵�)");
            LoadEndingScene(2); // 3�� ������ �ε�
            return;
        }

        // 4. ���� �Ǵ� ����� ����
        if (player.date >= 30)
        {
            Debug.Log("���� ���� ����: ����� ���� (3�� ������ �̵�)");
            LoadEndingScene(2); // 3�� ������ �ε�
        }
        else
        {
            Debug.Log("���� ���� ������");
        }
    }

    private void LoadEndingScene(int sceneIndex)
    {
        Debug.Log($"���� �� �ε�: {sceneIndex}�� ��");
        if (sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex); // �� �ε����� ����Ͽ� ��ȯ
        }
        else
        {
            Debug.LogError($"�� {sceneIndex}��(��) ���� ������ �߰����� �ʾҽ��ϴ�.");
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
        SceneManager.LoadScene("GameStartScene"); // ���� �� �ε�
    }

    public void QuitGame()
    {
        Debug.Log("���� ����");
        Application.Quit(); // ���� ���� ���忡�� �۵�
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
        Debug.Log("�÷��̾� ������ �ʱ�ȭ��");
    }

    private void UpdateUI()
    {
        // �ʿ��� UI�� ������Ʈ (�ʵ� �߰� ����)
    }
}