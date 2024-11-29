using System.IO;
using UnityEngine;

[System.Serializable]
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

    public bool[] purchasedItems; // 구매한 아이템 상태 저장

    public PlayerData()
    {
        purchasedItems = new bool[10]; // 예제: 10개의 아이템 (아이템 개수는 필요에 맞게 조정)
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();
    public string path { get; private set; } // 저장 경로
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
            return;
        }

        path = Application.persistentDataPath + "/save"; // 저장 경로 설정
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
            nowPlayer = new PlayerData(); // 기본 데이터로 초기화
        }
    }
}
