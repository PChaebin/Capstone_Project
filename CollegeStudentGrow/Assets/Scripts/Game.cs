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
        activityButton.SetActive(true);  // 활동 버튼 활성화

        storeUI.SetActive(false);
        storeButton.SetActive(true);  // 상점 버튼
    }

    public void PerformActivity() // 활동하기 버튼 클릭 시 호출
    {
        bool isActive = activityUI.activeSelf;
        activityUI.SetActive(!isActive);

    }
    public void PerformStore() // 상점 버튼 클릭 시 호출
    {
        bool isActive = storeUI.activeSelf;
        storeUI.SetActive(!isActive);
    }

    public void ExitStore() // 상점 버튼 클릭 시 호출
    {
        // Check if the store UI is active
        bool isActive = storeUI.activeSelf;

        // If it's active, deactivate it (close the store)
        if (isActive)
        {
            storeUI.SetActive(false);
        }
    }


    // 학교가기 버튼 클릭 시 호출
    public void GoToSchool()
    {
        activityManager.GoToSchool();
    }

    // 알바하기 버튼 클릭 시 호출
    public void DoPartTimeJob()
    {
        activityManager.DoPartTimeJob();
    }

    // 놀러가기 버튼 클릭 시 호출
    public void GoOnTrip()
    {
        activityManager.GoOnTrip();
    }

    // 휴식하기 버튼 클릭 시 호출
    public void Rest()
    {
        activityManager.Rest();
    }
    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
