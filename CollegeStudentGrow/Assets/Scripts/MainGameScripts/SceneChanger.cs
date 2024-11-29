using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // 페이드 효과를 위한 CanvasGroup
    public Animator fadeAnimator; // Animator로 페이드 효과 제어
    public float fadeTime = 2f; // 페이드 인/아웃 시간
    public bool isFadeInOnStart = true; // 시작 시 페이드 인 여부

    private int levelToLoad; // 로드할 씬 인덱스

    private void Start()
    {
        if (canvasGroup != null)
        {
            // CanvasGroup을 사용하는 경우
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                canvasGroup.alpha = 1f; // 초기 알파 값 설정
            }
            else if (isFadeInOnStart)
            {
                canvasGroup.alpha = 0f; // 시작 시 페이드 인
                LeanTween.alphaCanvas(canvasGroup, 1f, fadeTime).setEase(LeanTweenType.easeInOutQuad);
            }
        }
        else if (fadeAnimator != null)
        {
            // Animator를 사용하는 경우
            if (isFadeInOnStart)
            {
                fadeAnimator.SetTrigger("FadeIn");
            }
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex; // 전환할 씬 인덱스 설정

        if (canvasGroup != null)
        {
            // CanvasGroup을 사용하는 페이드 아웃
            LeanTween.alphaCanvas(canvasGroup, 0f, fadeTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                SceneManager.LoadScene(levelToLoad); // 씬 로드
            });
        }
        else if (fadeAnimator != null)
        {
            // Animator를 사용하는 페이드 아웃
            StartCoroutine(LoadLevel(levelIndex));
        }
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut"); // 페이드 아웃 트리거
            yield return new WaitForSeconds(1f); // 페이드 아웃 대기 시간
        }

        SceneManager.LoadScene(levelIndex); // 씬 전환
    }
}