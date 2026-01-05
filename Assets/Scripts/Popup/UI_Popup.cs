using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        Managers.UI_Manager.SetCanvas(gameObject, true);
    }
}
