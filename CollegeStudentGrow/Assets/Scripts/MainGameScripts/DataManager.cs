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
    public string endingType = ""; // 엔딩 타입 저장 ("success", "expelled", "bankrupt", "stress")
    public bool[] purchasedItems;

    public PlayerData()
    {
        purchasedItems = new bool[10];
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
        Debug.Log($"데이터 저장 완료: {filePath}");
    }

    public void LoadData()
    {
        string filePath = $"{path}{nowSlot}.json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log($"데이터 로드 완료: {filePath}");
        }
        else
        {
            Debug.LogWarning($"로드 실패: {filePath} 파일이 없습니다. 기본 데이터 사용.");
            nowPlayer = new PlayerData();
        }
    }
}
