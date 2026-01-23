using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx // 씬매니저 확장
{
    public BaseScene CurentScene // 현재 scene이 tile lobby game 컴포넌트인지 확인 
    {
        get
        {
           return GameObject.FindFirstObjectByType<BaseScene>();
        }
    }
    public void LoadScene(Define.Scene type) // 래핑함수 
    {
        CurentScene.Clear();
        SceneManager.LoadScene(type.ToString());
    }
}
