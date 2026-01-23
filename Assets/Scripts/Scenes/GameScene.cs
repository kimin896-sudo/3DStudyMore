using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene // Game 씬 선봉장 
{
    protected override void Init()
    {
        base.Init();

        // _scnenType = Define.Scene.Game; // 현재 _scnenType 을 Game타입으로 변경 
        SceneType = Define.Scene.Game; // 현재 _scnenType 을 Game타입으로 변경 


        //Managers.UI_Manager.ShowSceneUI<UI_Inven>("UI_Inven"); // 씬에 관련된 UI니까 Scene Class 에서 관리하겠다.

        /*        for (int i = 0; i < 5; i++)
                {
                    Managers.Resources.Instantiate("Player");
                }*/

        Dictionary<int,Stat> dict =  Managers.DataManager.StatDioct;

    }
    public override void Clear()
    {
        Loger.Log("LobbyScene CLear");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Title);
            //SceneManager.LoadScene("Title"); // 동기 방식 씬 모드
        }
    }
}
