using UnityEngine;
using TMPro;

public class ActivityManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text stressText;
    public TMP_Text dateText;
    public TMP_Text levelText;

    private int activityCount = 0; 

    // 학교가기
    public void GoToSchool()
    {
        Debug.Log("학교 가기 선택됨");
        DataManager.instance.nowPlayer.stress += 5;  // 스트레스 증가
        CompleteActivity();  // 활동 후 처리
    }

    // 알바하기
    public void DoPartTimeJob()
    {
        Debug.Log("알바하기 선택됨");
        DataManager.instance.nowPlayer.coin += 20;    // 동전 증가
        DataManager.instance.nowPlayer.stress += 3;   // 스트레스 증가
        CompleteActivity();  // 활동 후 처리
    }

    // 놀러가기
    public void GoOnTrip()
    {
        Debug.Log("놀러가기 선택됨");
        DataManager.instance.nowPlayer.coin -= 15;    // 동전 감소
        DataManager.instance.nowPlayer.stress -= 5;   // 스트레스 감소

        // 음수값 방지
        DataManager.instance.nowPlayer.coin = Mathf.Max(DataManager.instance.nowPlayer.coin, 0);
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        CompleteActivity();  // 활동 후 처리
    }

    // 휴식하기
    public void Rest()
    {
        Debug.Log("휴식하기 선택됨");
        DataManager.instance.nowPlayer.stress -= 10;  // 스트레스 감소

        // 스트레스가 음수로 내려가지 않도록
        DataManager.instance.nowPlayer.stress = Mathf.Max(DataManager.instance.nowPlayer.stress, 0);

        CompleteActivity(); 
    }

    private void CompleteActivity() // 날짜 갱신
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
