using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    public GameObject prefab;
    void Start()
    {
        // 생성을 하는 쪽에서 디스트로이도 해줘야함 
        // 다른 곳에서 삭제하면 관리하기에 까다로움
/*        prefab = Resources.Load<GameObject>("Prefabs/Car");
        Object.Instantiate(prefab);

        Destroy(prefab, 0.5f);*/

        prefab = Managers.Resources.Instantiate("Car");

        Managers.Destroy(prefab,5f);
        // 이렇게 여러개 만들기에 불편하기에 ResourceManager를 만들어줌

    }


}
