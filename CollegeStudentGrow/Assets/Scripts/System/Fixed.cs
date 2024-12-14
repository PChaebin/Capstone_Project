using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    // ������ ���� (��: 9:16)
    private float targetAspectRatio = 9.0f / 16.0f;

    private void Awake()
    {
        // �� GameObject�� ���� �����ص� �ı����� �ʵ��� ����
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetResolution();
    }

    /// <summary>
    /// �ػ� ���� �Լ�
    /// </summary>
    public void SetResolution()
    {
        // ���� ȭ�� ���� ���
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // ������ ������ ������ ��ġ�ϵ��� ȭ�� ũ�� ����
        if (currentAspectRatio > targetAspectRatio)
        {
            // ȭ���� ���η� �ʹ� �� ���
            float width = Screen.height * targetAspectRatio;
            Screen.SetResolution((int)width, Screen.height, true);
        }
        else
        {
            // ȭ���� ���η� �ʹ� �� ���
            float height = Screen.width / targetAspectRatio;
            Screen.SetResolution(Screen.width, (int)height, true);
        }
    }
}

