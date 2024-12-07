using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat; // 이름 입력 UI
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName; // 이름 입력 필드
    public SceneChanger sceneChanger; // SceneChanger 스크립트 참조
    private bool[] savefile = new bool[3]; // 슬롯별 데이터 존재 여부

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 확인
        for (int i = 0; i < 3; i++)
        {
            string filePath = DataManager.instance.path + $"{i}.json";
            if (File.Exists(filePath))
            {
                savefile[i] = true; // 저장 파일이 존재하는 슬롯
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData(); // 데이터 로드
                slotText[i].text = DataManager.instance.nowPlayer.name; // 슬롯 이름 표시
            }
            else
            {
                slotText[i].text = "Empty"; // 비어 있는 슬롯
            }
        }

        creat.SetActive(false); // 이름 입력 UI 비활성화
    }

    public void Slot(int number) // 슬롯 선택
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number]) // 저장된 데이터가 있는 슬롯
        {
            DataManager.instance.LoadData(); // 데이터 로드
            StartGame(); // 게임 시작
        }
        else
        {
            // 저장된 데이터가 없는 경우 이름 입력 UI 활성화
            creat.SetActive(true);
        }
    }

    public void CreateNewPlayerData() // 새로운 데이터 생성
    {
        string playerName = newPlayerName.text.Trim(); // 입력된 이름 가져오기

        if (string.IsNullOrWhiteSpace(playerName)) // 이름이 비어 있는지 확인
        {
            return; // 이름이 없으면 중단
        }

        // 새로운 플레이어 데이터 생성 및 저장
        DataManager.instance.nowPlayer = new PlayerData
        {
            name = playerName
        };
        DataManager.instance.SaveData();

        // 이름 입력 UI 비활성화
        creat.SetActive(false);

        StartGame(); // 게임 시작
    }

    private void StartGame()
    {
        sceneChanger.FadeToLevel(1); // 다음 씬으로 전환
    }
}
