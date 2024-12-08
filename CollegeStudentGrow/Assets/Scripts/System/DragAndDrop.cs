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
        Debug.LogError("BeverageMaking ��ũ��Ʈ�� ã��.");
        if (beverageMaking == null)
        {
            Debug.LogError("BeverageMaking ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetAsLastSibling();
        DefaultPos = this.transform.position;
        isOverCup = false;
        Debug.Log("�巡�� ����");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
        Debug.Log("�巡����");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(!isOverCup && beverageMaking != null)
        {
            beverageMaking.CheckIngredient(this.gameObject.name);
            Debug.Log("���� ��� Ȯ�� �ڵ� ����");
        }
        this.transform.position = DefaultPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cup"))
        {
            isOverCup = true;
            Debug.Log("���̶� �浹");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Cup"))
        {
            isOverCup = false;
            Debug.Log("���̶� �浹 ���");
        }
    }
}
