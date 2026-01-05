using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{

    // 리스너 패턴
    public Action keyAction = null;

    public Action<Define.MouseEvent> MouseAction = null;
    // 매개변수가 필요없는 

    bool isPressed = false;
    public void OnUpdate()
    {
        // 어떤 키가 눌리지 않았을때 
        if(Input.anyKey == false) return;
        if (keyAction != null) keyAction.Invoke(); // 그럼 key 브로드캐스트 하자


        // 드레그 or 클릭 판단. 
        if(MouseAction !=null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                isPressed = true;
            }
            else
            {
                if (isPressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }

                isPressed = false;
            }
        }
      
    }
}
