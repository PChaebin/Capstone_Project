using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject storeUI; // ���� UI

    // ������ �� �� ȣ��Ǵ� �Լ�
    public void OpenStore()
    {
        storeUI.SetActive(true); // ���� UI�� Ȱ��ȭ
    }

    // ������ ���� �� ȣ��Ǵ� �Լ�
    public void CloseStore()
    {
        storeUI.SetActive(false); // ���� UI�� ��Ȱ��ȭ
    }
}
