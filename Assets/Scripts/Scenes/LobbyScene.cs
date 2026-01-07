using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : BaseScene
{
    public override void Clear()
    {
        Loger.Log("LobbyScene CLear");
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Lobby; // 현재 _scnenType 을 Game타입으로 변경 


     //   Managers.UI_Manager.ShowSceneUI<UI_Inven>("UI_Inven"); // 씬에 관련된 UI니까 Scene Class 에서 관리하겠다.

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
           // SceneManager.LoadScene("Game"); // 동기 방식 씬 모드
        }
    }
}
