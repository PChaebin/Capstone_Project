using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//�����ϴ� ���
//1. ������ �����Ͱ� ����
//2. �����͸� ���̽����� ��ȯ
//3. ���̽��� �ܺο� ����

//�ҷ����� ���
//1. �ܺο� ����� ���̽��� ������
//2. ���̽��� ���������·� ��ȯ
//3. �ҷ��� �����͸� ���

public class PlayerData
{
    public string name; // �̸�
    public string grade;// ����

    public int coin = 10; //��
    public int stress = 0; // ��Ʈ����
    public int level = 1;   // �˹ٽ���
    public int rebrith = 0; // ȯ��Ƚ��
    public int date = 1; // ��¥
    public bool itemPurchased = false; // ������ ���� ����
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
