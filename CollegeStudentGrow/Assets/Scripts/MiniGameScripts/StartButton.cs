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
        Debug.Log($"로드된 스프라이트 개수: {sprites.Length}");

        sprite_1 = sprites.FirstOrDefault(sprite => sprite.name == "RecipeBook_1");
        sprite_2 = sprites.FirstOrDefault(sprite => sprite.name == "RecipeBook_2");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit 이벤트 발생");

        if (sprite_2 != null)
        {
            Image startButtonImg = gameObject.GetComponent<Image>();
            startButtonImg.sprite = sprite_2;
            Debug.Log("Start 버튼 이미지 변경 완료");
        }
        else
        {
            Debug.LogWarning("RecipeBook_2 스프라이트를 찾을 수 없습니다.");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter 이벤트 발생");        

        if (sprite_1 != null)
        {
            Image startButtonImg = gameObject.GetComponent<Image>();
            startButtonImg.sprite = sprite_1;
            Debug.Log("Start 버튼 이미지 변경 완료");
        }
        else
        {
            Debug.LogWarning("RecipeBook_1 스프라이트를 찾을 수 없습니다.");
        }
    }
}
