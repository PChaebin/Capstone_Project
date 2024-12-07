using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup으로 페이드 효과 구현
    public float fadeTime = 2f; // 페이드 인/아웃에 걸리는 시간

    private int levelToLoad; // 로드할 씬의 인덱스

    private void Start()
    {
        if (canvasGroup != null)
        {
            // 씬 시작 시 페이드인 효과 (알파값: 0 → 1)
            canvasGroup.alpha = 0f; // 시작은 투명
            StartCoroutine(FadeIn());
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex; // 전환할 씬의 인덱스를 저장
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = elapsedTime / fadeTime; // 알파값: 0 → 1
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // 페이드인이 완료되면 알파값을 1로 설정
    }

    private IEnumerator FadeOutAndLoad()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = 1f - (elapsedTime / fadeTime); // 알파값: 1 → 0
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // 페이드아웃 완료 후 알파값을 0으로 설정

        SceneManager.LoadScene(levelToLoad);
    }
}
