using UnityEngine;
using System.IO;

public class PlayerData
{
    public string name;  // 이름
    public string grade; // 학점

    public int coin = 10;    // 돈
    public int stress = 0;   // 스트레스
    public int level = 1;    // 레벨
    public int rebrith = 0;  // 환생 횟수
    public int date = 1;     // 날짜
    public int score = 0;    // 점수
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
        Debug.Log($"데이터 저장 완료: {filePath}");
    }

    public void LoadData()
    {
        string filePath = path + nowSlot.ToString() + ".json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log($"데이터 로드 완료: {filePath}, 이름: {nowPlayer.name}");
        }
        else
        {
            Debug.LogWarning($"로드 실패: {filePath} 파일이 존재하지 않습니다.");
        }
    }
}
