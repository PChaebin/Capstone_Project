using UnityEngine;
using TMPro;

public class GaME : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text date;
    public TMP_Text level;
    public TMP_Text coin;
    public TMP_Text stress;

    public GameObject activityUI;
    public GameObject activityButton;
    public ActivityManager activityManager;

    void Start()
    {
        name.text += DataManager.instance.nowPlayer.name;
        level.text += DataManager.instance.nowPlayer.level.ToString();
        coin.text += DataManager.instance.nowPlayer.coin.ToString();
        date.text += DataManager.instance.nowPlayer.date.ToString();
        stress.text += DataManager.instance.nowPlayer.stress.ToString();

        activityUI.SetActive(false);
        activityButton.SetActive(true);  // Ȱ�� ��ư Ȱ��ȭ
    }

    // Ȱ���ϱ� ��ư Ŭ�� �� ȣ��
    public void PerformActivity()
    {
        // Ȱ�� UI Ȱ��ȭ, ��ư �����
        activityUI.SetActive(true);
        activityButton.SetActive(true);
    }

    // �б����� ��ư Ŭ�� �� ȣ��
    public void GoToSchool()
    {
        activityManager.GoToSchool();
    }

    // �˹��ϱ� ��ư Ŭ�� �� ȣ��
    public void DoPartTimeJob()
    {
        activityManager.DoPartTimeJob();
    }

    // ����� ��ư Ŭ�� �� ȣ��
    public void GoOnTrip()
    {
        activityManager.GoOnTrip();
    }

    // �޽��ϱ� ��ư Ŭ�� �� ȣ��
    public void Rest()
    {
        activityManager.Rest();
    }

    // ���� �Լ�
    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
