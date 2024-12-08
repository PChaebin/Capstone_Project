using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour
{
    public Image uiImage; // UI 이미지 컴포넌트
    public Sprite[] animationFrames1; // 첫 번째 애니메이션 프레임들
    public Sprite[] animationFrames2; // 두 번째 애니메이션 프레임들
    public float animationSpeed = 0.1f; // 프레임 전환 속도

    private int currentFrame;
    private float timer;
    private Sprite[] currentAnimationFrames; // 현재 애니메이션을 나타낼 변수
    private int repeatCount = 0; // 현재 애니메이션의 반복 횟수 추적
    private const int maxRepeats = 5; // 각 애니메이션의 최대 반복 횟수

    void Start()
    {
        // 첫 번째 애니메이션을 시작
        currentAnimationFrames = animationFrames1;
        currentFrame = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % currentAnimationFrames.Length;
            uiImage.sprite = currentAnimationFrames[currentFrame];

            // 애니메이션이 한 사이클 끝났을 때
            if (currentFrame == 0)
            {
                repeatCount++; // 반복 횟수 증가

                // 첫 번째 애니메이션이 5번 반복된 후 두 번째 애니메이션으로 전환
                if (repeatCount >= maxRepeats && currentAnimationFrames == animationFrames1)
                {
                    currentAnimationFrames = animationFrames2; // 두 번째 애니메이션으로 변경
                    repeatCount = 0; // 반복 횟수 리셋
                }
                // 두 번째 애니메이션이 5번 반복된 후 첫 번째 애니메이션으로 전환
                else if (repeatCount >= maxRepeats && currentAnimationFrames == animationFrames2)
                {
                    currentAnimationFrames = animationFrames1; // 첫 번째 애니메이션으로 변경
                    repeatCount = 0; // 반복 횟수 리셋
                }
            }
        }
    }
}
