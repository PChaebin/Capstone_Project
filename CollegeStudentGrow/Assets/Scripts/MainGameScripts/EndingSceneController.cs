using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class EndingSceneController : MonoBehaviour
{
    public TMP_Text endingMessage; // ���� �޽����� ǥ���� �ؽ�Ʈ
    public CanvasGroup fadeCanvasGroup; // ȭ�� ��ü ���̵� ȿ���� CanvasGroup
    public Image image1; // ù ��° �̹���
    public Image image2; // �� ��° �̹���
    public float fadeTime = 2f; // ���̵� �ð�
    public float delayTime = 2f; // �� ���� �� ��� �ð�

    void Start()
    {
        // ���� �� ���İ� �ʱ�ȭ
        fadeCanvasGroup.alpha = 0f;

        // �̹��� ��Ȱ��ȭ
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        // �÷��̾� ������ �ε�
        PlayerData player = DataManager.instance.nowPlayer;

        // ���� �޽��� ����
        if (player.endingType == "success")
        {
            endingMessage.text = "�����մϴ�! �������� ���� ��Ȱ�� ���ƽ��ϴ�!";
            StartCoroutine(StartWithFadeIn());
        }
        else
        {
            endingMessage.text = "���� ������ �������� �ʾҽ��ϴ�.";
        }
    }

    private IEnumerator StartWithFadeIn()
    {
        
        // ó�� ���̵� �� ȿ�� ����
        yield return StartCoroutine(FadeIn());

        // ù ��° �̹��� Ȱ��ȭ
        image1.gameObject.SetActive(true);
        
        // ��� �� ù ��° �̹������� �� ��° �̹����� ��ȯ
        yield return new WaitForSeconds(delayTime);
        yield return StartCoroutine(FadeOut());

        // �̹��� ��ȯ
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(true);

        // �� ��° �̹��� ���̵� ��
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            fadeCanvasGroup.alpha = 1f - (elapsedTime / fadeTime); // ���İ�: 1 �� 0
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f; // ������ ����
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            fadeCanvasGroup.alpha = elapsedTime / fadeTime; // ���İ�: 0 �� 1
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f; // ������ ����
    }
}
