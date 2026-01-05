using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_EventHandler : MonoBehaviour,IBeginDragHandler,IDragHandler,IPointerClickHandler
{
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;


    private Vector2 offset; // 마우스 클릭 지점과 이미지 중심의 차이 저장
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnBeginDragHandler != null)
            OnBeginDragHandler.Invoke(eventData);

        // 현재 마우스 위치에서 오브젝트의 현재 위치를 빼서 거리를 구합니다.
       // offset = (Vector2)transform.position - eventData.position;
    }

    // 드래그 중일 때 계속 호출됩니다.
    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
        // 현재 마우스 위치에 처음에 계산한 거리(offset)를 더해줍니다.
        //transform.position = eventData.position + offset;
       // Debug.Log("드래그 중...");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }
}
