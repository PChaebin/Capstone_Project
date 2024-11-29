using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    [SerializeField] private float tweenTime;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform RecipePagesRect;
    [SerializeField] LeanTweenType tweenType;

    private int CurrentPage;
    private Vector3 targetPos;
    private float dragThreshould;

    private void Awake()
    {
        Debug.Log(Screen.width / 35);
        CurrentPage = 1;
        targetPos = RecipePagesRect.localPosition;
        dragThreshould = Screen.width / 35;
    }

    public void Next()
    {
        if(CurrentPage < maxPage)
        {
            CurrentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    private void MovePage()
    {
        if (RecipePagesRect == null)
        {
            Debug.LogError("RecipePagesRect가 null입니다. 올바르게 설정되었는지 확인하세요.");
            return;
        }

        RecipePagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RecipePagesRect == null)
        {
            Debug.LogError("RecipePagesRect가 null입니다. 드래그 이벤트를 처리할 수 없습니다.");
            return;
        }

        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x < eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }

    private void OnDestroy()
    {
        if (RecipePagesRect != null)
        {
            LeanTween.cancel(RecipePagesRect.gameObject);
        }
    }
}
