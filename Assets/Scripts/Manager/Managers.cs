using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance; // 유일성이 보장 된다 / 클래스에 종속적
    public static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }
    InputManager _inputManager = new InputManager(); // 단일성을 위해서 매니저 클래스에서만 생성 
    // 개체를 접근 할 수 있도록 열어줌
    ResourceManager _resourceMnager = new ResourceManager(); // 단일성을 위해서 매니저 클래스에서만 생성 
    UI_Manager _ui_manger = new UI_Manager();
    SceneManagerEx _sceneManagerEx = new SceneManagerEx();
    SoundManger _soundManger = new SoundManger();
    PoolManager _poolManager = new PoolManager();
    DataManager _dataManager = new DataManager();

    public static DataManager DataManager
    {
        get { return Instance._dataManager; }
    }    
    public static PoolManager PoolManager
    {
        get { return Instance._poolManager; }
    }

    public static SoundManger SoundManager
    {
        get { return Instance._soundManger; }
    }
    public static SceneManagerEx Scene
    {
        get
        {
            return Instance._sceneManagerEx;
        }
    }
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
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManagers");

            if (go == null)
            {
                go = new GameObject { name = "@GameManagers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._soundManger.Init(); // 앞으로 start에서 못 할땐 여기서 하자 

            s_instance._poolManager.Init(); // poolManager.Init 

            s_instance._dataManager.Init(); //  

        }
    }

    public static void Clear() // 풀매니저
    {
        PoolManager.clear();
    }
}



