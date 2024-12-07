using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingSceneController : MonoBehaviour
{
    public TMP_Text endingMessage; // ���� �޽����� ǥ���� �ؽ�Ʈ
    public CanvasGroup fadeCanvasGroup; // ȭ�� ��ü ���̵� ȿ���� CanvasGroup
    public Image image1; // ù ��° �̹���
    public Image image2; // �� ��° �̹���

    public Image image3; // ù ��° �̹���
    public Image image4; // �� ��° �̹���

    public Image image5; // �� ��° �̹���

    public Image image6; // �� ��° �̹���

    public Image image7; // �� ��° �̹���


    public float fadeTime = 2f; // ���̵� �ð�
    public float delayTime = 2f; // �� ���� �� ��� �ð�

    public Button rebirthButton; // ȯ�� ��ư
    public Button quitButton; // ���� ��ư
    public PlayerData playerData; // �÷��̾� ������ ����

    void Start()
    {
        rebirthButton.onClick.AddListener(OnRebirthClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        // ���� �� ���İ� �ʱ�ȭ
        fadeCanvasGroup.alpha = 0f;

        // �̹��� ��Ȱ��ȭ
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        image3.gameObject.SetActive(false);
        image4.gameObject.SetActive(false);

        // ��ư ��Ȱ��ȭ
        rebirthButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // �÷��̾� ������ �ε�
        PlayerData player = DataManager.instance.nowPlayer;

        // ���� �޽��� ����
        if (player.endingType == "success")
        {
            endingMessage.text = "�����մϴ�! �������� ���� ��Ȱ�� ���ƽ��ϴ�!";
            StartCoroutine(StartWithFadeIn());
        }
        else if (player.endingType == "normal")
        {
            endingMessage.text = "��� ����!";
            StartCoroutine(StartWithFadeIn_normal());
        }
        else if (player.endingType == "expelled")
        {
            endingMessage.text = "���� ����";
            StartCoroutine(StartWithFadeIn_false());
        }
        else if(player.endingType == "bankrupt")
        {
            endingMessage.text = "���� ����";
            StartCoroutine(StartWithFadeIn_false_2());
        }
        else if(player.endingType == "stress")
        {
            endingMessage.text = "��Ʈ���� ���� ����";
            StartCoroutine(StartWithFadeIn_false_3());
        }
        else
        {
            endingMessage.text = "���� ������ �������� �ʾҽ��ϴ�.";
        }
    }

    private IEnumerator StartWithFadeIn()
    {
        yield return new WaitForSeconds(delayTime);

        // �̹��� 1 ���̵���
        image1.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // �̹��� 1 ���̵�ƿ�
        yield return StartCoroutine(FadeOut());
        image1.gameObject.SetActive(false);

        // �̹��� 2 ���̵���
        image2.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        // ���� �޽��� ��� �� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_normal()
    {
        yield return new WaitForSeconds(delayTime);

        // �̹��� 3 ���̵���
        image3.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // �̹��� 3 ���̵�ƿ�
        yield return StartCoroutine(FadeOut());
        image3.gameObject.SetActive(false);

        // �̹��� 4 ���̵���
        image4.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        // ���� �޽��� ��� �� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false()
    {
        yield return new WaitForSeconds(delayTime);

        // �̹��� 5 ���̵���
        image5.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // ���� �޽��� ��� �� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false_2()
    {
        yield return new WaitForSeconds(delayTime);

        // �̹��� 6 ���̵���
        image6.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // ���� �޽��� ��� �� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false_3()
    {
        yield return new WaitForSeconds(delayTime);

        // �̹��� 6 ���̵���
        image7.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // ���� �޽��� ��� �� ��ư Ȱ��ȭ
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }




    private IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeTime)
        {
            fadeCanvasGroup.alpha = time / fadeTime;
            time += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeTime)
        {
            fadeCanvasGroup.alpha = 1f - (time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }

    public void OnRebirthClicked()
    {
        DataManager.instance.ResetPlayerData();
        SceneManager.LoadScene("Title"); // "TitleScene"�� Ÿ��Ʋ �� �̸����� ����
    }

    public void OnQuitClicked()
    {
        // ���� ��ư Ŭ�� �� ���� ����
        DataManager.instance.ResetPlayerData();
        Application.Quit();
    }
}
