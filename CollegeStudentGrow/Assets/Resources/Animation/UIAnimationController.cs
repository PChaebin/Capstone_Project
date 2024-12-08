using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationController : MonoBehaviour
{
    public Image uiImage; // UI �̹��� ������Ʈ
    public Sprite[] animationFrames1; // ù ��° �ִϸ��̼� �����ӵ�
    public Sprite[] animationFrames2; // �� ��° �ִϸ��̼� �����ӵ�
    public float animationSpeed = 0.1f; // ������ ��ȯ �ӵ�

    private int currentFrame;
    private float timer;
    private Sprite[] currentAnimationFrames; // ���� �ִϸ��̼��� ��Ÿ�� ����
    private int repeatCount = 0; // ���� �ִϸ��̼��� �ݺ� Ƚ�� ����
    private const int maxRepeats = 5; // �� �ִϸ��̼��� �ִ� �ݺ� Ƚ��

    void Start()
    {
        // ù ��° �ִϸ��̼��� ����
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

            // �ִϸ��̼��� �� ����Ŭ ������ ��
            if (currentFrame == 0)
            {
                repeatCount++; // �ݺ� Ƚ�� ����

                // ù ��° �ִϸ��̼��� 5�� �ݺ��� �� �� ��° �ִϸ��̼����� ��ȯ
                if (repeatCount >= maxRepeats && currentAnimationFrames == animationFrames1)
                {
                    currentAnimationFrames = animationFrames2; // �� ��° �ִϸ��̼����� ����
                    repeatCount = 0; // �ݺ� Ƚ�� ����
                }
                // �� ��° �ִϸ��̼��� 5�� �ݺ��� �� ù ��° �ִϸ��̼����� ��ȯ
                else if (repeatCount >= maxRepeats && currentAnimationFrames == animationFrames2)
                {
                    currentAnimationFrames = animationFrames1; // ù ��° �ִϸ��̼����� ����
                    repeatCount = 0; // �ݺ� Ƚ�� ����
                }
            }
        }
    }
}
