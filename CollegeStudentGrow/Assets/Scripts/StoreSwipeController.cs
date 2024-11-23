using UnityEngine;
using UnityEngine.UI;

public class StoreSwipeController : MonoBehaviour
{
    [Header("ScrollRect Settings")]
    [SerializeField] private ScrollRect scrollRect; // ScrollRect ������Ʈ
    [SerializeField] private RectTransform contentRect; // Content�� RectTransform
    [SerializeField] private float pageTransitionTime = 0.01f; // ������ ��ȯ �ð� (��)
    [SerializeField] private int totalPages = 4; // �� ������ ��
    [SerializeField] private LeanTweenType animationType = LeanTweenType.easeInOutQuad; // LeanTween �ִϸ��̼� Ÿ��

    private int currentPage = 0; // ���� ������ (0���� ����)

    private void Start()
    {
        // �ʵ� ��ȿ�� �˻�
        if (scrollRect == null || contentRect == null)
        {
            Debug.LogError("ScrollRect �Ǵ� Content RectTransform�� ������� �ʾҽ��ϴ�. Inspector���� Ȯ���ϼ���.");
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

        // LeanTween���� �ε巯�� ��ũ�� �ִϸ��̼�
        LeanTween.value(scrollRect.gameObject, scrollRect.horizontalNormalizedPosition, targetPosition, pageTransitionTime)
            .setEase(animationType)
            .setOnUpdate((float value) =>
            {
                scrollRect.horizontalNormalizedPosition = value; // ScrollRect�� ��ġ ������Ʈ
            });

        Debug.Log($"������ �̵� �Ϸ�: {pageIndex}, ��ǥ ��ġ: {targetPosition}");
    }
}
