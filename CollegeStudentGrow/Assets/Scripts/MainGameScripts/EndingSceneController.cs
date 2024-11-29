using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class EndingSceneController : MonoBehaviour
{
    public TMP_Text endingMessage; // 엔딩 메시지를 표시할 텍스트
    public CanvasGroup fadeCanvasGroup; // 화면 전체 페이드 효과용 CanvasGroup
    public Image image1; // 첫 번째 이미지
    public Image image2; // 두 번째 이미지
    public float fadeTime = 2f; // 페이드 시간
    public float delayTime = 2f; // 각 상태 간 대기 시간

    void Start()
    {
        // 시작 시 알파값 초기화
        fadeCanvasGroup.alpha = 0f;

        // 이미지 비활성화
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        // 플레이어 데이터 로드
        PlayerData player = DataManager.instance.nowPlayer;

        // 엔딩 메시지 설정
        if (player.endingType == "success")
        {
            endingMessage.text = "축하합니다! 성공적인 대학 생활을 마쳤습니다!";
            StartCoroutine(StartWithFadeIn());
        }
        else
        {
            endingMessage.text = "엔딩 조건이 만족되지 않았습니다.";
        }
    }

    private IEnumerator StartWithFadeIn()
    {
        
        // 처음 페이드 인 효과 실행
        yield return StartCoroutine(FadeIn());

        // 첫 번째 이미지 활성화
        image1.gameObject.SetActive(true);
        
        // 대기 후 첫 번째 이미지에서 두 번째 이미지로 전환
        yield return new WaitForSeconds(delayTime);
        yield return StartCoroutine(FadeOut());

        // 이미지 전환
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(true);

        // 두 번째 이미지 페이드 인
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            fadeCanvasGroup.alpha = 1f - (elapsedTime / fadeTime); // 알파값: 1 → 0
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f; // 완전히 투명
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            fadeCanvasGroup.alpha = elapsedTime / fadeTime; // 알파값: 0 → 1
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f; // 완전히 보임
    }
}
