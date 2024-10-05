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
        //슬롯별로 저장된 데이터가 존재하는지 판단
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

    public void Slot(int number)// 슬롯마다 불러오기
    {
        DataManager.instance.nowSlot = number;
        // 1. 저장된 데이터가 없을때 
        if (savefile[number])
        {
            // 2. 저장된 데이터가 있을때 -> 불러오기해서 게임씬으로 넘어감.
            DataManager.instance.LoadData();
            GoGame(); // 씬 전환
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
        if (!savefile[DataManager.instance.nowSlot]) // 슬롯에 없을시
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }

        SceneManager.LoadScene(1);
    }
}
