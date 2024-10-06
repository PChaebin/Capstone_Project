using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class GaME : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text date;
    public TMP_Text level;
    public TMP_Text coin;
    public TMP_Text stress;
    public GameObject storeUI;

    //test item
    public GameObject itemImage;
   

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(); // 

        //Test item : Check Purchase Status
        if (DataManager.instance.nowPlayer.itemPurchased)
        {
            itemImage.SetActive(true); // 
        }
    }

    public void UpdateUI()
    {
        name.text = "Name: " + DataManager.instance.nowPlayer.name;
        level.text = "Level: " + DataManager.instance.nowPlayer.level.ToString();
        coin.text = "Coin: " + DataManager.instance.nowPlayer.coin.ToString();
        date.text = "Date: " + DataManager.instance.nowPlayer.date.ToString();
        stress.text = "Stress: " + DataManager.instance.nowPlayer.stress.ToString();
    }

    public void LevelUp()
    {
        DataManager.instance.nowPlayer.level++;
        UpdateUI(); 
    }

    public void CoinUp()
    {
        DataManager.instance.nowPlayer.coin += 10;
        UpdateUI();
    }

    public void DateUp() // Logic Not Yet Implemented
    {
        DataManager.instance.nowPlayer.date++; 
        UpdateUI(); 
    }

    public void Save()
    {
        DataManager.instance.SaveData();
        UpdateUI();
    }


    public void OpenStore()
    {
        storeUI.SetActive(true); // store iu active
    }

    
    public void CloseStore()
    {
        storeUI.SetActive(false); // store iu close
    }

    
    public void BuyItem(int price)// Function to Purchase Item
    {
  
        if (DataManager.instance.nowPlayer.coin >= price)
        {
            DataManager.instance.nowPlayer.coin -= price;
            Debug.Log(price + "원짜리 아이템을 구매했습니다. 남은 돈: " + DataManager.instance.nowPlayer.coin);

            //test item
            DataManager.instance.nowPlayer.itemPurchased = true;
            itemImage.SetActive(true); 
          
            UpdateUI();

            // data save
            DataManager.instance.SaveData();
        }
        else
        {
            Debug.Log("돈이 부족합니다!");
        }
    }
}
