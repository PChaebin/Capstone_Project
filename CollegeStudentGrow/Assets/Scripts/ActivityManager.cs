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
    public GameObject countdownPanel; // ī��Ʈ�ٿ� â
    public TMP_Text countdownText;    // ī��Ʈ�ٿ� �ؽ�Ʈ
    public Button[] actionButtons;    // Ȱ�� ��ư��
    public Button storeButton;        // ���� ��ư

    public TMP_Text gradeText; // ���� UI �ؽ�Ʈ
    public TMP_Text scoreText; // ���ھ� �ؽ�Ʈ

    private int activityCount = 0;

    private void Start()
    {
        countdownPanel.SetActive(false); // ���� �� ��Ȱ��ȭ
        UpdateUI(); // �ʱ� UI ������Ʈ
    }

    public void GoToSchool()
    {
        Debug.Log("�б� ���� ���õ�");
        DataManager.instance.nowPlayer.stress += 5;  // ��Ʈ���� ����
        DataManager.instance.nowPlayer.score += 5; // ���ھ� ����
        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    private IEnumerator ShowCountdownPanel()
    {
        SetButtonsInteractable(false); // ��� ��ư ��Ȱ��ȭ

        countdownPanel.SetActive(true); // ī��Ʈ�ٿ� Ȱ��ȭ
        int countdown = 5;

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // ī��Ʈ�ٿ� ������Ʈ
            yield return new WaitForSeconds(1);        // 1�� ���
            countdown--;
        }

        countdownPanel.SetActive(false); // ī��Ʈ�ٿ� ��Ȱ��ȭ
        SetButtonsInteractable(true);    // ��ư �ٽ� Ȱ��ȭ
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in actionButtons)
        {
            button.interactable = interactable; // Ȱ�� ��ư ��ȣ�ۿ� ����
        }

        if (storeButton != null)
        {
            storeButton.interactable = interactable; // ���� ��ư ��ȣ�ۿ� ����
        }
    }

    public void DoPartTimeJob()
    {
        Debug.Log("�˹��ϱ� ���õ�");
        DataManager.instance.nowPlayer.coin += 20;
        DataManager.instance.nowPlayer.stress += 3;
        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    public void GoOnTrip()
    {
        Debug.Log("����� ���õ�");
        DataManager.instance.nowPlayer.coin -= 15;
        DataManager.instance.nowPlayer.stress -= 5;

        DataManager.instance.nowPlayer.coin = Mathf.Max(DataManager.instance.nowPlayer.coin, 0);
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        StartCoroutine(ShowCountdownPanel());
        CompleteActivity();
    }

    public void Rest()
    {
        Debug.Log("�޽��ϱ� ���õ�");
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
        DataManager.instance.SaveData(); // Ȱ�� �� �ڵ� ����
        FindObjectOfType<EndingManager>().CheckEnding(); // ���� ���� Ȯ��
    }

    private void DateUp()
    {
        DataManager.instance.nowPlayer.date++;
        Debug.Log("Date Updated: " + DataManager.instance.nowPlayer.date); // �����
        DataManager.instance.SaveData(); // ��¥ ������Ʈ �� ����
        dateText.text = "��¥: " + DataManager.instance.nowPlayer.date.ToString();
    }

    private void UpdateUI()
    {
        PlayerData player = DataManager.instance.nowPlayer;

        // ���� ���
        player.grade = CalculateGrade(player.score);

        // UI ������Ʈ
        coinText.text = player.coin.ToString();
        stressText.text = "Stress : " + player.stress.ToString();
        dateText.text = player.date.ToString();
        levelText.text = player.level.ToString();
        gradeText.text = "Grade : " + player.grade;
        scoreText.text = "Score : " + player.score.ToString();
    }

    private string CalculateGrade(int score)
    {
        if (score >= 95) return "A+";
        else if (score >= 85) return "A";
        else if (score >= 75) return "B+";
        else if (score >= 65) return "B";
        else if (score >= 55) return "C+";
        else if (score >= 45) return "C";
        else if (score >= 35) return "D+";
        else if (score >= 25) return "D";
        else return "F";
    }
}
