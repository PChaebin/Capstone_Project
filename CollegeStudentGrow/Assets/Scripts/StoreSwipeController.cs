using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSwipeController : MonoBehaviour
{
    [SerializeField] private int maxPage;
    [SerializeField] private float tweenTime;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] private RectTransform RecipePagesRect;
    [SerializeField] LeanTweenType tweenType;

    private int CurrentPage;
    private Vector3 targetPos;

    private void Awake()
    {
        CurrentPage = 1;
        targetPos = RecipePagesRect.localPosition;
    }

    public void Next()
    {
        if (CurrentPage < maxPage)
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
}
