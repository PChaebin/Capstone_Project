using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public TMP_Text endingText; // ���� ��� �ؽ�Ʈ
    public GameObject endingPanel; // ���� UI �г�
    public ActivityManager activityManager; // ActivityManager ����

    private PlayerData player;
    private int stressOverThresholdDays = 0; // ��Ʈ���� ���� ī����
    private const int stressThreshold = 90;  // ��Ʈ���� ���� ����
    private int lowCoinDays = 0;
    void Start()
    {
        player = DataManager.instance.nowPlayer; // ���� �÷��̾� ������ ��������
        if (endingPanel == null || endingText == null)
        {
            Debug.LogError("EndingPanel �Ǵ� EndingText�� Inspector���� �������� �ʾҽ��ϴ�.");
            return;
        }

        if (activityManager == null)
        {
            Debug.LogError("ActivityManager�� Inspector���� �������� �ʾҽ��ϴ�.");
            return;
        }

        Debug.Log("���� ��¥ (player.date): " + player.date);
        endingPanel.SetActive(false); // ���� �� ���� UI ��Ȱ��ȭ
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding ȣ���: ���� ��¥ = {player.date}, ���� = {player.score}, ���� = {player.grade}");

        string title = "";
        string message = "";

        // 1. ���� ���� - ���� ����
        if (player.date >= 30 && player.score < 60)
        {
            int schoolActivities = activityManager.GetActivityCount(); // �б� ���� Ȱ�� Ƚ�� ��������

            if (schoolActivities < 18)
            {
                title = "���� ����";
                message = "��: �� ���� �б� ���� ��������... ���� ����?\n\n(�� ����)\n\n(�� �տ� ���̹о��� ���� ������)\n\n����: �� �� �̰� ����?";
                ShowEnding(title, message);
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
            title = "����� �Ѱܳ� �Ͱ� ����";
            message = "������: 304ȣ ������ �� ���Խ��ϴ�. �� ���ּ���.\n\n(Ư.��.���)";
            ShowEnding(title, message);
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
            title = "���޽� �� ����";
            message = "��ȣ��: ȯ�ں� ������ �弼��?\n\n�ǻ�: ��Ʈ���� ���ٷ� ���� ȱ���Դϴ�. ġ�ᰡ �ʿ��մϴ�.";
            ShowEnding(title, message);
            return;
        }

        // 4. ���� �Ǵ� ����� ����
        if (player.date >= 30)
        {
            if (player.grade == "A+" && player.score >= 95)
            {
                title = "���� ���� ����";
                message = "�����մϴ�! A+ �������� ������ �����߽��ϴ�!";
            }
            else if (player.grade == "B" || player.grade == "B+")
            {
                title = "�߼ұ�� ���� ����";
                message = "���� �������� �߼ұ���� �����߽��ϴ�!";
            }
            else
            {
                title = "����� �λ�";
                message = "����� ���� ��ҽ��ϴ�.";
            }

            ShowEnding(title, message);
        }
        else
        {
            Debug.Log("���� ���� ������");
        }
    }

    private void ShowEnding(string title, string message)
    {
        endingPanel.SetActive(true);
        endingText.text = $"[{title}]\n\n{message}";
        Time.timeScale = 0; // ���� ����
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
