using System.IO;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public string name;
    public string grade;
    public int coin = 10;
    public int stress = 0;
    public int level = 1;
    public int rebrith = 0;
    public int date = 1;
    public int score = 0;
    public string endingType = ""; // 엔딩 타입 저장
    public bool[] purchasedItems;

    public PlayerData()
    {
        purchasedItems = new bool[10]; // 아이템 배열 기본 크기
    }

    // 데이터 초기화 (아이템 배열과 엔딩 타입 제외)
    public void ResetData()
    {
        coin = 10;
        score = 0;
        grade = "";
        date = 1;
        level = 1;
        rebrith += 1;
        stress = 0;

        // purchasedItems, endingType은 초기화하지 않음
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData nowPlayer = new PlayerData();
    public string path { get; private set; }
    public int nowSlot = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string filePath = $"{path}{nowSlot}.json";
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(filePath, data);
    }

    public void LoadData()
    {
        string filePath = $"{path}{nowSlot}.json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);

            // 엔딩 타입이 비어 있으면 기본값 설정
            if (string.IsNullOrEmpty(nowPlayer.endingType))
            {
                nowPlayer.endingType = "normal"; // 예시로 기본값을 설정
            }
        }
        else
        {
            nowPlayer = new PlayerData();
        }
    }

    public void ResetPlayerData()
    {
        // 아이템 배열과 엔딩 타입을 제외한 나머지 데이터 초기화
        nowPlayer.ResetData();
        SaveData(); // 초기화된 데이터를 저장
    }

}
