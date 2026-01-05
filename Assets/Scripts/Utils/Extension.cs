

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension // 개체가 없는 클래스 
{
    // 랩핑과 굉장히 유사함

    // this 
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)// 개체가 필요없는 정적인 함수 
    {
        UI_Base.AddUIEvent(go, action, type);
    }
}
