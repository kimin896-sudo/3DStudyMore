using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance; // 유일성이 보장 된다 / 클래스에 종속적
    public static Managers Instance
    {
        get
        { 
            Init();
            return instance;
        }
    }
    InputManager _inputManager = new InputManager(); // 단일성을 위해서 매니저 클래스에서만 생성 
    // 개체를 접근 할 수 있도록 열어줌
    ResourceManager _resourceMnager = new ResourceManager(); // 단일성을 위해서 매니저 클래스에서만 생성 

    UI_Manager _ui_manger = new UI_Manager();
    public static UI_Manager UI_Manager
    {
        get
        {
            return Instance._ui_manger;
        }
    }
    public static ResourceManager Resources
    {
        get
        {
            return Instance._resourceMnager;
        }
    }
    public static InputManager Input 
    { 
        get 
        { 
            return Instance._inputManager; 
        } 
    }
    private void Update()
    {
        _inputManager.OnUpdate();
    }
    void Start()
    {
        Init();

    }
    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@GameManagers");

            if (go == null)
            {
                go = new GameObject { name = "@GameManagers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();

        }
    }
}



