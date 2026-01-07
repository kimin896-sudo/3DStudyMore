using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Managers.UI_Manager.SetCanvas(gameObject, true);


    }

    public virtual void ClosePopUI() 
    {
        Managers.UI_Manager.ClosePopupUI(this);
    }
}
