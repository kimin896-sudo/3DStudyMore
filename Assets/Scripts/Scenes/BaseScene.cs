using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
// 인스팩터상에서 볼 수 없는 이유 = // 추상클래스는 인스턴스화 할 수 없음 
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; // C# 7.0부터 가능, 그 이전에선 불가능

    void Awake()
    {
        Init();
    }


    // 이 클래스를 상속받는 자식은 init을 더이상 오버라이드 할 수 없게 할려면sealed를 붙여야함
    protected virtual void Init() // 상속받은 자식이 수정가능한 함수 ~
    {
        Object obj = GameObject.FindFirstObjectByType(typeof(EventSystem));

        if (obj == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";

    }
    public abstract void Clear();
    
}
