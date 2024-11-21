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

    public GameObject storeUI;
    public GameObject storeButton;

    public ActivityManager activityManager;

    void Start()
    {
        // UI �ʱ�ȭ
        name.text = "Name: " + DataManager.instance.nowPlayer.name;
        level.text = "Level: " + DataManager.instance.nowPlayer.level.ToString();
        coin.text = "Coin: " + DataManager.instance.nowPlayer.coin.ToString();
        date.text = "Date: " + DataManager.instance.nowPlayer.date.ToString();
        stress.text = "Stress: " + DataManager.instance.nowPlayer.stress.ToString();

        activityUI.SetActive(false);
        activityButton.SetActive(true);

        storeUI.SetActive(false);
        storeButton.SetActive(true);
    }

    public void PerformActivity() // Ȱ���ϱ� ��ư Ŭ�� �� ȣ��
    {
        bool isActive = activityUI.activeSelf;
        activityUI.SetActive(!isActive);
    }

    public void PerformStore() // ���� ��ư Ŭ�� �� ȣ��
    {
        bool isActive = storeUI.activeSelf;
        storeUI.SetActive(!isActive);
    }

    public void ExitStore()
    {
        if (storeUI.activeSelf)
        {
            storeUI.SetActive(false);
        }
    }

    // �б����� ��ư
    public void GoToSchool()
    {
        activityManager.GoToSchool();
    }

    // �˹��ϱ� ��ư
    public void DoPartTimeJob()
    {
        activityManager.DoPartTimeJob();
    }

    // ����� ��ư
    public void GoOnTrip()
    {
        activityManager.GoOnTrip();
    }

    // �޽��ϱ� ��ư
    public void Rest()
    {
        activityManager.Rest();
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
