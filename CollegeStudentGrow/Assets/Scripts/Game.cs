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
    public StoreManager store;

    void Start()
    {
        name.text += DataManager.instance.nowPlayer.name;
        level.text += DataManager.instance.nowPlayer.level.ToString();
        coin.text += DataManager.instance.nowPlayer.coin.ToString();
        date.text += DataManager.instance.nowPlayer.date.ToString();
        stress.text += DataManager.instance.nowPlayer.stress.ToString();

        activityUI.SetActive(false);
        activityButton.SetActive(true);  // Ȱ�� ��ư Ȱ��ȭ

        storeUI.SetActive(false);
        storeButton.SetActive(true);  // ���� ��ư
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

    public void ExitStore() // ���� ��ư Ŭ�� �� ȣ��
    {
        // Check if the store UI is active
        bool isActive = storeUI.activeSelf;

        // If it's active, deactivate it (close the store)
        if (isActive)
        {
            storeUI.SetActive(false);
        }
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
    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
