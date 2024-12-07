using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;

public class Select : MonoBehaviour
{
    public GameObject creat; // �̸� �Է� UI
    public TMP_Text[] slotText;
    public TMP_Text newPlayerName; // �̸� �Է� �ʵ�
    public SceneChanger sceneChanger; // SceneChanger ��ũ��Ʈ ����
    private bool[] savefile = new bool[3]; // ���Ժ� ������ ���� ����

    void Start()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� Ȯ��
        for (int i = 0; i < 3; i++)
        {
            string filePath = DataManager.instance.path + $"{i}.json";
            if (File.Exists(filePath))
            {
                savefile[i] = true; // ���� ������ �����ϴ� ����
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData(); // ������ �ε�
                slotText[i].text = DataManager.instance.nowPlayer.name; // ���� �̸� ǥ��
            }
            else
            {
                slotText[i].text = "Empty"; // ��� �ִ� ����
            }
        }

        creat.SetActive(false); // �̸� �Է� UI ��Ȱ��ȭ
    }

    public void Slot(int number) // ���� ����
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number]) // ����� �����Ͱ� �ִ� ����
        {
            DataManager.instance.LoadData(); // ������ �ε�
            StartGame(); // ���� ����
        }
        else
        {
            // ����� �����Ͱ� ���� ��� �̸� �Է� UI Ȱ��ȭ
            creat.SetActive(true);
        }
    }

    public void CreateNewPlayerData() // ���ο� ������ ����
    {
        string playerName = newPlayerName.text.Trim(); // �Էµ� �̸� ��������

        if (string.IsNullOrWhiteSpace(playerName)) // �̸��� ��� �ִ��� Ȯ��
        {
            return; // �̸��� ������ �ߴ�
        }

        // ���ο� �÷��̾� ������ ���� �� ����
        DataManager.instance.nowPlayer = new PlayerData
        {
            name = playerName
        };
        DataManager.instance.SaveData();

        // �̸� �Է� UI ��Ȱ��ȭ
        creat.SetActive(false);

        StartGame(); // ���� ����
    }

    private void StartGame()
    {
        sceneChanger.FadeToLevel(1); // ���� ������ ��ȯ
    }
}
