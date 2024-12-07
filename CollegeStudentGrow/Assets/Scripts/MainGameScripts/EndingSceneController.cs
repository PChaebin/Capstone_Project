using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingSceneController : MonoBehaviour
{
    public TMP_Text endingMessage; // 엔딩 메시지를 표시할 텍스트
    public CanvasGroup fadeCanvasGroup; // 화면 전체 페이드 효과용 CanvasGroup
    public Image image1; // 첫 번째 이미지
    public Image image2; // 두 번째 이미지

    public Image image3; // 첫 번째 이미지
    public Image image4; // 두 번째 이미지

    public Image image5; // 두 번째 이미지

    public Image image6; // 두 번째 이미지

    public Image image7; // 두 번째 이미지


    public float fadeTime = 2f; // 페이드 시간
    public float delayTime = 2f; // 각 상태 간 대기 시간

    public Button rebirthButton; // 환생 버튼
    public Button quitButton; // 종료 버튼
    public PlayerData playerData; // 플레이어 데이터 참조

    void Start()
    {
        rebirthButton.onClick.AddListener(OnRebirthClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        // 시작 시 알파값 초기화
        fadeCanvasGroup.alpha = 0f;

        // 이미지 비활성화
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        image3.gameObject.SetActive(false);
        image4.gameObject.SetActive(false);

        // 버튼 비활성화
        rebirthButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        // 플레이어 데이터 로드
        PlayerData player = DataManager.instance.nowPlayer;

        // 엔딩 메시지 설정
        if (player.endingType == "success")
        {
            endingMessage.text = "축하합니다! 성공적인 대학 생활을 마쳤습니다!";
            StartCoroutine(StartWithFadeIn());
        }
        else if (player.endingType == "normal")
        {
            endingMessage.text = "노멀 엔딩!";
            StartCoroutine(StartWithFadeIn_normal());
        }
        else if (player.endingType == "expelled")
        {
            endingMessage.text = "제적 엔딩";
            StartCoroutine(StartWithFadeIn_false());
        }
        else if(player.endingType == "bankrupt")
        {
            endingMessage.text = "거지 엔딩";
            StartCoroutine(StartWithFadeIn_false_2());
        }
        else if(player.endingType == "stress")
        {
            endingMessage.text = "스트레스 과다 엔딩";
            StartCoroutine(StartWithFadeIn_false_3());
        }
        else
        {
            endingMessage.text = "엔딩 조건이 만족되지 않았습니다.";
        }
    }

    private IEnumerator StartWithFadeIn()
    {
        yield return new WaitForSeconds(delayTime);

        // 이미지 1 페이드인
        image1.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // 이미지 1 페이드아웃
        yield return StartCoroutine(FadeOut());
        image1.gameObject.SetActive(false);

        // 이미지 2 페이드인
        image2.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        // 엔딩 메시지 출력 후 버튼 활성화
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_normal()
    {
        yield return new WaitForSeconds(delayTime);

        // 이미지 3 페이드인
        image3.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // 이미지 3 페이드아웃
        yield return StartCoroutine(FadeOut());
        image3.gameObject.SetActive(false);

        // 이미지 4 페이드인
        image4.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());

        // 엔딩 메시지 출력 후 버튼 활성화
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false()
    {
        yield return new WaitForSeconds(delayTime);

        // 이미지 5 페이드인
        image5.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // 엔딩 메시지 출력 후 버튼 활성화
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false_2()
    {
        yield return new WaitForSeconds(delayTime);

        // 이미지 6 페이드인
        image6.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // 엔딩 메시지 출력 후 버튼 활성화
        yield return new WaitForSeconds(delayTime);
        rebirthButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private IEnumerator StartWithFadeIn_false_3()
    {
        yield return new WaitForSeconds(delayTime);

        // 이미지 6 페이드인
        image7.gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(delayTime);

        // 엔딩 메시지 출력 후 버튼 활성화
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
        SceneManager.LoadScene("Title"); // "TitleScene"을 타이틀 씬 이름으로 변경
    }

    public void OnQuitClicked()
    {
        // 종료 버튼 클릭 시 게임 종료
        DataManager.instance.ResetPlayerData();
        Application.Quit();
    }
}
