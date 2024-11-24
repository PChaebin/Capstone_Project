using UnityEngine;
using UnityEngine.UI;

public class StoreSwipeController : MonoBehaviour
{
    [Header("ScrollRect Settings")]
    [SerializeField] private ScrollRect scrollRect; 
    [SerializeField] private RectTransform contentRect; 
    [SerializeField] private float pageTransitionTime = 0.01f; // ������ ��ȯ �ð�)
    [SerializeField] private int totalPages = 4;
    [SerializeField] private LeanTweenType animationType = LeanTweenType.easeInOutQuad;
    [SerializeField] private StoreManager storeManager; 

    private int currentPage = 0; // ���� ������

    private void Start()
    {
        if (scrollRect == null || contentRect == null)
        {
            Debug.LogError("ScrollRect �Ǵ� Content RectTransform�� ������� �ʾҽ��ϴ�. Inspector���� Ȯ���ϼ���.");
        }
        if (storeManager == null)
        {
            Debug.LogError("StoreManager�� ������� �ʾҽ��ϴ�. Inspector���� Ȯ���ϼ���.");
        }
    }

    /// <summary>
    /// ���� �������� �̵��մϴ�.
    /// </summary>
    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            MoveToPage(currentPage);

            // UI ������Ʈ
            storeManager.UpdateCurrentPage(currentPage);
        }
        else
        {
            Debug.Log("������ �������Դϴ�.");
        }
    }

    /// <summary>
    /// ���� �������� �̵��մϴ�.
    /// </summary>
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            MoveToPage(currentPage);

            // UI ������Ʈ
            storeManager.UpdateCurrentPage(currentPage);
        }
        else
        {
            Debug.Log("ù ��° �������Դϴ�.");
        }
    }

    /// <summary>
    /// ������ �������� �̵��մϴ�.
    /// </summary>
    /// <param name="pageIndex">�̵��� ������ �ε���</param>
    private void MoveToPage(int pageIndex)
    {
        // ��ǥ ��ġ ��� (0.0 ~ 1.0 ������ ��)
        float targetPosition = (float)pageIndex / (totalPages - 1);

        LeanTween.value(scrollRect.gameObject, scrollRect.horizontalNormalizedPosition, targetPosition, pageTransitionTime)
            .setEase(animationType)
            .setOnUpdate((float value) =>
            {
                scrollRect.horizontalNormalizedPosition = value;
            });

        Debug.Log($"������ �̵�: {pageIndex}, ��ǥ ��ġ: {targetPosition}");
    }
}
