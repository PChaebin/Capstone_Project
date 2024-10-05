using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class GaME : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text date;
    public TMP_Text level;
    public TMP_Text coin;
    public TMP_Text stress;

    // Start is called before the first frame update
    void Start()
    {
        name.text += DataManager.instance.nowPlayer.name;
        level.text += DataManager.instance.nowPlayer.level.ToString();
        coin.text += DataManager.instance.nowPlayer.coin.ToString();
        date.text += DataManager.instance.nowPlayer.date.ToString();
        stress.text += DataManager.instance.nowPlayer.stress.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelUp()
    {
        DataManager.instance.nowPlayer.level++;
        level.text = "Level : " + DataManager.instance.nowPlayer.level.ToString();
    }

    public void CoinUp()
    {
        DataManager.instance.nowPlayer.coin += 10;
        coin.text = "Coin : " + DataManager.instance.nowPlayer.coin.ToString();
    }

    public void DateUp()
    {
       
    }

    public void Save()
    {
        DataManager.instance.SaveData();
    }
}
