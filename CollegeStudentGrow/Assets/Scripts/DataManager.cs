using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//저장하는 방법
//1. 저장할 데이터가 존재
//2. 데이터를 제이슨으로 변환
//3. 제이슨을 외부에 저장

//불러오는 방법
//1. 외부에 저장된 제이슨을 가져옴
//2. 제이슨을 데이터형태로 변환
//3. 불러온 데이터를 사용

public class PlayerData
{
    public string name; // 이름
    public string grade;// 성적

    public int coin = 10; //돈
    public int stress = 0; // 스트레스
    public int level = 1;   // 알바스탯
    public int rebrith = 0; // 환생횟수
    public int date = 1; // 날짜
    public bool itemPurchased = false; // 아이템 구매 여부
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public  PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    void Start()
    {
       
        
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path+ nowSlot.ToString(), data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
