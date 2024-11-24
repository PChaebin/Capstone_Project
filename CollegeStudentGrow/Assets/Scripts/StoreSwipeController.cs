using UnityEngine;
using UnityEngine.UI;

public class StoreSwipeController : MonoBehaviour
{
    [Header("ScrollRect Settings")]
    [SerializeField] private ScrollRect scrollRect; 
    [SerializeField] private RectTransform contentRect; 
    [SerializeField] private float pageTransitionTime = 0.01f; // 페이지 전환 시간)
    [SerializeField] private int totalPages = 4;
    [SerializeField] private LeanTweenType animationType = LeanTweenType.easeInOutQuad;
    [SerializeField] private StoreManager storeManager; 

    private int currentPage = 0; // 현재 페이지

    private void Start()
    {
        if (scrollRect == null || contentRect == null)
        {
            Debug.LogError("ScrollRect 또는 Content RectTransform이 연결되지 않았습니다. Inspector에서 확인하세요.");
        }
        if (storeManager == null)
        {
            Debug.LogError("StoreManager가 연결되지 않았습니다. Inspector에서 확인하세요.");
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

            // UI 업데이트
            storeManager.UpdateCurrentPage(currentPage);
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

            // UI 업데이트
            storeManager.UpdateCurrentPage(currentPage);
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

        LeanTween.value(scrollRect.gameObject, scrollRect.horizontalNormalizedPosition, targetPosition, pageTransitionTime)
            .setEase(animationType)
            .setOnUpdate((float value) =>
            {
                scrollRect.horizontalNormalizedPosition = value;
            });

        Debug.Log($"페이지 이동: {pageIndex}, 목표 위치: {targetPosition}");
    }
}
