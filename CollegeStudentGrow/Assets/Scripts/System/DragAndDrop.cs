using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Vector2 DefaultPos;

    private BeverageMaking beverageMaking;

    private bool isOverCup = false;

    private void Start()
    {
       beverageMaking = FindObjectOfType<BeverageMaking>();
        Debug.LogError("BeverageMaking 스크립트를 찾음.");
        if (beverageMaking == null)
        {
            Debug.LogError("BeverageMaking 스크립트를 찾을 수 없습니다.");
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetAsLastSibling();
        DefaultPos = this.transform.position;
        isOverCup = false;
        Debug.Log("드래그 시작");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
        Debug.Log("드래그중");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(!isOverCup && beverageMaking != null)
        {
            beverageMaking.CheckIngredient(this.gameObject.name);
            Debug.Log("음료 재료 확인 코드 실행");
        }
        this.transform.position = DefaultPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            isOverCup = true;
            Debug.Log("컵이랑 충돌");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Cup"))
        {
            isOverCup = false;
            Debug.Log("컵이랑 충돌 벗어남");
        }
    }
}
