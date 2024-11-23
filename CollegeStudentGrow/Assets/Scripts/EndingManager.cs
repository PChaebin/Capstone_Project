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

    public TMP_Text endingText; // ���� ��� �ؽ�Ʈ
    public GameObject endingPanel; // ���� UI �г�
    private PlayerData player;

    void Start()
    {
        player = DataManager.instance.nowPlayer; // ���� �÷��̾� ������ ��������
        if (endingPanel == null || endingText == null)
        {
            Debug.LogError("EndingPanel �Ǵ� EndingText�� Inspector���� �������� �ʾҽ��ϴ�.");
            return;
        }
        Debug.Log("���� ��¥ (player.date): " + player.date);
        endingPanel.SetActive(false); // ���� �� ���� UI ��Ȱ��ȭ
    }

    public void CheckEnding()
    {
        Debug.Log($"CheckEnding ȣ���: ���� ��¥ = {player.date}, ���� = {player.score}, ���� = {player.grade}");

        if (player.date >= 30) // ��¥ ���� ����
        {
            string title = "";
            string message = "";

            if (player.grade == "A+" && player.score >= 95) // ���� ������
            {
                title = "���� ���� ����";
                message = "�����մϴ�! A+ �������� ������ �����߽��ϴ�!";
            }
            else if (player.grade == "B" || player.grade == "B+") // ���� ������
            {
                title = "�߼ұ�� ���� ����";
                message = "���� �������� �߼ұ���� �����߽��ϴ�!";
            }
            else // ����� �λ� �޽���
            {
                title = "����� �λ�";
                message = "����� ���� ��ҽ��ϴ�.";
            }

            ShowEnding(title, message); // �� �ڵ尡 ����� ���ɼ��� ����
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
        coinText.text = "����: " + player.coin.ToString();
        dateText.text = "��¥: " + player.date.ToString();
        levelText.text = "����: " + player.level.ToString();
        stressText.text = "��Ʈ����: " + player.stress.ToString();
        scoreText.text = "����: " + player.score.ToString();
        gradeText.text = "����: " + player.grade;
    }
}
