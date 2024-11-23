using UnityEngine;
using UnityEngine.UI;

public class StoreSwipeController : MonoBehaviour
{
    [Header("ScrollRect Settings")]
    [SerializeField] private ScrollRect scrollRect; // ScrollRect 컴포넌트
    [SerializeField] private RectTransform contentRect; // Content의 RectTransform
    [SerializeField] private float pageTransitionTime = 0.01f; // 페이지 전환 시간 (초)
    [SerializeField] private int totalPages = 4; // 총 페이지 수
    [SerializeField] private LeanTweenType animationType = LeanTweenType.easeInOutQuad; // LeanTween 애니메이션 타입

    private int currentPage = 0; // 현재 페이지 (0부터 시작)

    private void Start()
    {
        // 필드 유효성 검사
        if (scrollRect == null || contentRect == null)
        {
            Debug.LogError("ScrollRect 또는 Content RectTransform이 연결되지 않았습니다. Inspector에서 확인하세요.");
        }
    }

    /// <summary>
    /// 다음 페이지로 이동합니다.
    /// </summary>
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            MoveToPage(currentPage);
        }
        else
        {
            Debug.Log("마지막 페이지입니다.");
        }
    }

    /// <summary>
    /// 이전 페이지로 이동합니다.
    /// </summary>
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            MoveToPage(currentPage);
        }
        else
        {
            Debug.Log("첫 번째 페이지입니다.");
        }
    }

    /// <summary>
    /// 지정한 페이지로 이동합니다.
    /// </summary>
    /// <param name="pageIndex">이동할 페이지 인덱스</param>
    private void MoveToPage(int pageIndex)
    {
        // 목표 위치 계산 (0.0 ~ 1.0 사이의 값)
        float targetPosition = (float)pageIndex / (totalPages - 1);

        // LeanTween으로 부드러운 스크롤 애니메이션
        LeanTween.value(scrollRect.gameObject, scrollRect.horizontalNormalizedPosition, targetPosition, pageTransitionTime)
            .setEase(animationType)
            .setOnUpdate((float value) =>
            {
                scrollRect.horizontalNormalizedPosition = value; // ScrollRect의 위치 업데이트
            });

        Debug.Log($"페이지 이동 완료: {pageIndex}, 목표 위치: {targetPosition}");
    }
}
