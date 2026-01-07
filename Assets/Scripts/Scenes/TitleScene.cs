using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class TitleScene : BaseScene
{
    public override void Clear()
    {
        Loger.Log("TitleScene CLear");
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Title; // 현재 _scnenType 을 Game타입으로 변경 
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby);
            //SceneManager.LoadScene("Lobby"); // 동기 방식 씬 모드
        }
    }


}
