using UnityEngine;
using TMPro;

public class ActivityManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text stressText;
    public TMP_Text dateText;
    public TMP_Text levelText;

    private int activityCount = 0; 

    // �б�����
    public void GoToSchool()
    {
        Debug.Log("�б� ���� ���õ�");
        DataManager.instance.nowPlayer.stress += 5;  // ��Ʈ���� ����
        CompleteActivity();  // Ȱ�� �� ó��
    }

    // �˹��ϱ�
    public void DoPartTimeJob()
    {
        Debug.Log("�˹��ϱ� ���õ�");
        DataManager.instance.nowPlayer.coin += 20;    // ���� ����
        DataManager.instance.nowPlayer.stress += 3;   // ��Ʈ���� ����
        CompleteActivity();  // Ȱ�� �� ó��
    }

    // �����
    public void GoOnTrip()
    {
        Debug.Log("����� ���õ�");
        DataManager.instance.nowPlayer.coin -= 15;    // ���� ����
        DataManager.instance.nowPlayer.stress -= 5;   // ��Ʈ���� ����

        // ������ ����
        DataManager.instance.nowPlayer.coin = Mathf.Max(DataManager.instance.nowPlayer.coin, 0);
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        CompleteActivity();  // Ȱ�� �� ó��
    }

    // �޽��ϱ�
    public void Rest()
    {
        Debug.Log("�޽��ϱ� ���õ�");
        DataManager.instance.nowPlayer.stress -= 10;  // ��Ʈ���� ����

        // ��Ʈ������ ������ �������� �ʵ���
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        CompleteActivity(); 
    }

    private void CompleteActivity() // ��¥ ����
    {
        activityCount++;
        if (activityCount >= 2)  
        {
            DateUp();
            activityCount = 0; 
        }

        UpdateUI();  
    }

    private void DateUp()
    {
        DataManager.instance.nowPlayer.date++;
        dateText.text = "Date : " + DataManager.instance.nowPlayer.date.ToString();
    }

    private void UpdateUI()
    {
        coinText.text = "Coin : " + DataManager.instance.nowPlayer.coin.ToString();
        stressText.text = "Stress : " + DataManager.instance.nowPlayer.stress.ToString();
        dateText.text = "Date : " + DataManager.instance.nowPlayer.date.ToString();
        levelText.text = "Level : " + DataManager.instance.nowPlayer.level.ToString();
    }
}
