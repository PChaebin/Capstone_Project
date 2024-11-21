using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat;
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName;
    bool[] savefile = new bool[3];

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 3; i++)
        {
            string filePath = DataManager.instance.path + $"{i}.json";
            if (File.Exists(filePath))
            {
                savefile[i] = true; // 저장 파일이 존재하는 슬롯
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData(); // 데이터 로드
                slotText[i].text = DataManager.instance.nowPlayer.name; // 슬롯 이름 업데이트
                Debug.Log($"슬롯 {i}에 저장된 데이터 로드됨: {DataManager.instance.nowPlayer.name}");
            }
            else
            {
                slotText[i].text = "Empty"; // 비어 있는 슬롯
            }
        }
    }

    public void Slot(int number) // 슬롯 선택
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number]) // 저장된 데이터가 있는 슬롯
        {
            DataManager.instance.LoadData(); // 데이터 로드
            Debug.Log($"슬롯 {number} 데이터 불러오기 완료: {DataManager.instance.nowPlayer.name}");
            GoGame(); // 씬 전환
        }
        else
        {
            Creat();
        }
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true); // 새로운 플레이어 생성 UI 활성화
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot]) // 저장된 데이터가 없는 경우
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text; // 플레이어 이름 설정
            DataManager.instance.SaveData(); // 데이터 저장
            Debug.Log($"새로운 데이터 저장: {newPlayerName.text}");
        }

        SceneManager.LoadScene(1); // 게임 씬으로 전환
    }
}
