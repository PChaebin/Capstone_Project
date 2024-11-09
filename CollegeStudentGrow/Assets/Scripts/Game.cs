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
        activityButton.SetActive(true);  // 활동 버튼 활성화
    }

    // 활동하기 버튼 클릭 시 호출
    public void PerformActivity()
    {
        // 활동 UI 활성화, 버튼 숨기기
        activityUI.SetActive(true);
        activityButton.SetActive(true);
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

    // 저장 함수
    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
