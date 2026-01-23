using UnityEngine;

// MonoBehaviour를 안 붙이는 이유는 안에 있는 기능을 사용할 필요가 없기 떄문에 
public class ResourceManager 
{
    
    
    public T Load<T>(string path) where T : UnityEngine.Object // 유니티에 있는 오브젝트만 넘어오게 
    {
        // ex) Prefab/knight/Darkight  Darkight를 사용하겠다
        if (typeof(T) == typeof(GameObject)) // 게임오브젝트가 T라는 얘기는 프리팹을 얘가 불러오라고 하는구나 
        {
            string name = path;
            int index = name.LastIndexOf('/');// path사용시 
            if(index >= 0)
            {
                name = name.Substring(index + 1); // 인덱스 다음자리부터 잘라서 가져오겠다.
            }

            GameObject go = Managers.PoolManager.GetOriginal(name);
            if (go != null)
            {
                return go as T;
            }
        }

        // 넘겨주는 주소를 통해서 Resources 폴더에 있는 오브젝트를 리턴하겠다.
        return Resources.Load<T>(path); 

    }

    // [래핑 함수]: 유니티의 기본 기능을 우리 프로젝트에 맞게 '재포장'한 함수입니다.
    // 이 함수를 쓰면 외부에서는 복잡한 과정 없이 이름만으로 물체를 만들 수 있습니다.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 1. [로드]: 공통 경로(prefab/)를 미리 붙여서 파일 위치를 찾습니다.
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        // 2. [예외 처리]: 만약 파일이 없으면 '불러오기 실패'라고 친절하게 알려줍니다. (안정성 강화)
        if (original == null)
        {
            Loger.Log($"{path} 불러오기 실패! 경로를 확인하세요.");
            return null;
        }

        if (original.GetComponent<Poolable>() != null) // Poolable붙어있는 녀석이라면 
        {
            return Managers.PoolManager.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = go.name.Replace("(Clone)", "");

        return go;
    }

    public void Destory(GameObject obj, float t = 0f)
    {



        // 1. [방어 코드]: 삭제하려는 대상이 이미 비어있는지 확인합니다.
        if (obj == null)
        {
            return; // 대상이 없으면 아무것도 하지 않고 함수를 종료 (에러 방지)
        }


        Poolable poolable = obj.GetComponent<Poolable>();
        if(poolable != null) //Poolable이 붙어있다면 push 없다면 Destory
        {
            Managers.PoolManager.Push(poolable);
            return;
        }

        // 2. [기능 실행]: 실제 유니티 엔진에 파괴 명령을 전달합니다.
        // t는 '지연 시간'으로, 기본값이 0이라서 넣지 않으면 즉시 파괴됩니다.

        // 삭제가 아니라 풀링대상자 라면 풀매니저에게 보내버리기 
        Object.Destroy(obj, t);
    }
}
