using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat;
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName;
    public SceneChanger sceneChanger; // SceneChanger ��ũ��Ʈ ����
    bool[] savefile = new bool[3];

    void Start()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i < 3; i++)
        {
            string filePath = DataManager.instance.path + $"{i}.json";
            if (File.Exists(filePath))
            {
                savefile[i] = true; // ���� ������ �����ϴ� ����
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData(); // ������ �ε�
                slotText[i].text = DataManager.instance.nowPlayer.name; // ���� �̸� ������Ʈ
                Debug.Log($"���� {i}�� ����� ������ �ε��: {DataManager.instance.nowPlayer.name}");
            }
            else
            {
                slotText[i].text = "Empty"; // ��� �ִ� ����
            }
        }
    }

    public void Slot(int number) // ���� ����
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number]) // ����� �����Ͱ� �ִ� ����
        {
            DataManager.instance.LoadData(); // ������ �ε�
            Debug.Log($"���� {number} ������ �ҷ����� �Ϸ�: {DataManager.instance.nowPlayer.name}");
        }
        else
        {
            Creat(); // ���ο� ������ ����
        }

        // �� ��ȯ
        sceneChanger.FadeToLevel(1);
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true); // ���ο� �÷��̾� ���� UI Ȱ��ȭ
    }
}
