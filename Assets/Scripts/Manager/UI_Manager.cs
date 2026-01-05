using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager
{

    int _order = 0; // 가장 최근에 사용한 소트오더를 저장할 예정

    // 생성된 팝업을 들고 있어 자료구조, LIFO(LAST IN FIRST OUT)
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); 

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas =  Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        canvas.overrideSorting = true; // 자신만의 sortingover를 하겠다. 캔버스 안에 캔버스가 있을 경우
        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }

    }

    public T ShowPopUI<T>(string name = null) where T : UI_Popup
    {
        // 이름이 널이라면 타입 이름으로 바꿔줌
        if(string .IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go =   Managers.Resources.Instantiate($"UI/PopupUI/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

     
        return popup;
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup =  _popupStack.Pop(); // 꺼내서 사용후 삭제 
        Managers.Resources.Destory(popup.gameObject);
        popup = null; // 혹시 모르니 댕글링포인트 

        _order--;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Loger.Log("삭제하려는 팝업이 마지막 팝업이 아닙니다.");
            return;
        }

        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        UI_Popup popup = _popupStack.Pop(); // 꺼내서 사용후 삭제 

        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

}
