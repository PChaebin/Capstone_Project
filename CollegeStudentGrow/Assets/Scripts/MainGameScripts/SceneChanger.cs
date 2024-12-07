using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup���� ���̵� ȿ�� ����
    public float fadeTime = 2f; // ���̵� ��/�ƿ��� �ɸ��� �ð�

    private int levelToLoad; // �ε��� ���� �ε���

    private void Start()
    {
        if (canvasGroup != null)
        {
            // �� ���� �� ���̵��� ȿ�� (���İ�: 0 �� 1)
            canvasGroup.alpha = 0f; // ������ ����
            StartCoroutine(FadeIn());
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex; // ��ȯ�� ���� �ε����� ����
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = elapsedTime / fadeTime; // ���İ�: 0 �� 1
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // ���̵����� �Ϸ�Ǹ� ���İ��� 1�� ����
    }

    private IEnumerator FadeOutAndLoad()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = 1f - (elapsedTime / fadeTime); // ���İ�: 1 �� 0
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // ���̵�ƿ� �Ϸ� �� ���İ��� 0���� ����

        SceneManager.LoadScene(levelToLoad);
    }
}
