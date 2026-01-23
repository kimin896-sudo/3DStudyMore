using System.Collections.Generic;
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


/*        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            list.Add(Managers.Resources.Instantiate("Player"));
        }

        foreach(GameObject obj in list)
        {
            Managers.Resources.Destory(obj);
        }*/

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
