using UnityEngine;
using System.IO;

public class PlayerData
{
    public string name;  // �̸�
    public string grade; // ����

    public int coin = 10;    // ��
    public int stress = 0;   // ��Ʈ����
    public int level = 1;    // ����
    public int rebrith = 0;  // ȯ�� Ƚ��
    public int date = 1;     // ��¥
    public int score = 0;    // ����
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void SaveData()
    {
        string filePath = path + nowSlot.ToString() + ".json";
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(filePath, data);
        Debug.Log($"������ ���� �Ϸ�: {filePath}");
    }

    public void LoadData()
    {
        string filePath = path + nowSlot.ToString() + ".json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log($"������ �ε� �Ϸ�: {filePath}, �̸�: {nowPlayer.name}");
        }
        else
        {
            Debug.LogWarning($"�ε� ����: {filePath} ������ �������� �ʽ��ϴ�.");
        }
    }
}
