using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject storeUI; // 상점 UI

    // 상점을 열 때 호출되는 함수
    public void OpenStore()
    {
        storeUI.SetActive(true); // 상점 UI를 활성화
    }

    // 상점을 닫을 때 호출되는 함수
    public void CloseStore()
    {
        storeUI.SetActive(false); // 상점 UI를 비활성화
    }
}
