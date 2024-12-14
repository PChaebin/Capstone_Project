using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspectRatio : MonoBehaviour
{
    // 고정할 비율 (예: 9:16)
    private float targetAspectRatio = 9.0f / 16.0f;

    private void Awake()
    {
        // 이 GameObject가 씬을 변경해도 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetResolution();
    }

    /// <summary>
    /// 해상도 고정 함수
    /// </summary>
    public void SetResolution()
    {
        // 현재 화면 비율 계산
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // 비율이 설정된 비율과 일치하도록 화면 크기 조정
        if (currentAspectRatio > targetAspectRatio)
        {
            // 화면이 가로로 너무 길 경우
            float width = Screen.height * targetAspectRatio;
            Screen.SetResolution((int)width, Screen.height, true);
        }
        else
        {
            // 화면이 세로로 너무 길 경우
            float height = Screen.width / targetAspectRatio;
            Screen.SetResolution(Screen.width, (int)height, true);
        }
    }
}

