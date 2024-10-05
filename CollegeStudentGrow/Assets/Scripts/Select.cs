using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for(int i = 0; i<3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;

            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataManager.instance.DataClear();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slot(int number)// ���Ը��� �ҷ�����
    {
        DataManager.instance.nowSlot = number;
        // 1. ����� �����Ͱ� ������ 
        if (savefile[number])
        {
            // 2. ����� �����Ͱ� ������ -> �ҷ������ؼ� ���Ӿ����� �Ѿ.
            DataManager.instance.LoadData();
            GoGame(); // �� ��ȯ
        }
        else
        {
            Creat();
        }
    
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot]) // ���Կ� ������
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }

        SceneManager.LoadScene(1);
    }
}
