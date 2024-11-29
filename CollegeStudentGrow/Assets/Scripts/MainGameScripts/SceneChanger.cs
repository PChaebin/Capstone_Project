using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup canvasGroup; // ���̵� ȿ���� ���� CanvasGroup
    public Animator fadeAnimator; // Animator�� ���̵� ȿ�� ����
    public float fadeTime = 2f; // ���̵� ��/�ƿ� �ð�
    public bool isFadeInOnStart = true; // ���� �� ���̵� �� ����

    private int levelToLoad; // �ε��� �� �ε���

    private void Start()
    {
        if (canvasGroup != null)
        {
            // CanvasGroup�� ����ϴ� ���
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                canvasGroup.alpha = 1f; // �ʱ� ���� �� ����
            }
            else if (isFadeInOnStart)
            {
                canvasGroup.alpha = 0f; // ���� �� ���̵� ��
                LeanTween.alphaCanvas(canvasGroup, 1f, fadeTime).setEase(LeanTweenType.easeInOutQuad);
            }
        }
        else if (fadeAnimator != null)
        {
            // Animator�� ����ϴ� ���
            if (isFadeInOnStart)
            {
                fadeAnimator.SetTrigger("FadeIn");
            }
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex; // ��ȯ�� �� �ε��� ����

        if (canvasGroup != null)
        {
            // CanvasGroup�� ����ϴ� ���̵� �ƿ�
            LeanTween.alphaCanvas(canvasGroup, 0f, fadeTime).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
            {
                SceneManager.LoadScene(levelToLoad); // �� �ε�
            });
        }
        else if (fadeAnimator != null)
        {
            // Animator�� ����ϴ� ���̵� �ƿ�
            StartCoroutine(LoadLevel(levelIndex));
        }
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut"); // ���̵� �ƿ� Ʈ����
            yield return new WaitForSeconds(1f); // ���̵� �ƿ� ��� �ð�
        }

        SceneManager.LoadScene(levelIndex); // �� ��ȯ
    }
}