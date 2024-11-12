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
        RecipePagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.y - eventData.pressPosition.y) > dragThreshould)
        {
            if (eventData.position.y < eventData.pressPosition.y)
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
}
