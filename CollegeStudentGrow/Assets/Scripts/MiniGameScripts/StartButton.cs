using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sprite sprite_1;
    private Sprite sprite_2;

    private void Awake()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("MiniGame/RecipeBook");
        Debug.Log($"�ε�� ��������Ʈ ����: {sprites.Length}");

        sprite_1 = sprites.FirstOrDefault(sprite => sprite.name == "RecipeBook_1");
        sprite_2 = sprites.FirstOrDefault(sprite => sprite.name == "RecipeBook_2");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit �̺�Ʈ �߻�");

        if (sprite_2 != null)
        {
            Image startButtonImg = gameObject.GetComponent<Image>();
            startButtonImg.sprite = sprite_2;
            Debug.Log("Start ��ư �̹��� ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("RecipeBook_2 ��������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter �̺�Ʈ �߻�");        

        if (sprite_1 != null)
        {
            Image startButtonImg = gameObject.GetComponent<Image>();
            startButtonImg.sprite = sprite_1;
            Debug.Log("Start ��ư �̹��� ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("RecipeBook_1 ��������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
