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
    public string endingType = ""; // ���� Ÿ�� ���� ("success", "expelled", "bankrupt", "stress")
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
        Debug.Log($"������ ���� �Ϸ�: {filePath}");
    }

    public void LoadData()
    {
        string filePath = $"{path}{nowSlot}.json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log($"������ �ε� �Ϸ�: {filePath}");
        }
        else
        {
            Debug.LogWarning($"�ε� ����: {filePath} ������ �����ϴ�. �⺻ ������ ���.");
            nowPlayer = new PlayerData();
        }
    }
}
