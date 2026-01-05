using UnityEngine;

// MonoBehaviour를 안 붙이는 이유는 안에 있는 기능을 사용할 필요가 없기 떄문에 
public class ResourceManager 
{
    
    
    public T Load<T>(string path) where T : UnityEngine.Object // 유니티에 있는 오브젝트만 넘어오게 
    {

        // 넘겨주는 주소를 통해서 Resources 폴더에 있는 오브젝트를 리턴하겠다.
        return Resources.Load<T>(path); 

    }

    // [래핑 함수]: 유니티의 기본 기능을 우리 프로젝트에 맞게 '재포장'한 함수입니다.
    // 이 함수를 쓰면 외부에서는 복잡한 과정 없이 이름만으로 물체를 만들 수 있습니다.
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 1. [로드]: 공통 경로(prefab/)를 미리 붙여서 파일 위치를 찾습니다.
        GameObject prefab = Load<GameObject>($"prefabs/{path}");
        // 2. [예외 처리]: 만약 파일이 없으면 '불러오기 실패'라고 친절하게 알려줍니다. (안정성 강화)
        if (prefab == null)
        {
            Loger.Log($"{path} 불러오기 실패! 경로를 확인하세요.");
            return null;
        }
        // 3. [최종 실행]: 모든 준비가 끝났으니 실제 유니티 엔진의 기능을 호출하여 물체를 생성합니다.
        return Object.Instantiate(prefab, parent);
    }

    public void Destory(GameObject obj, float t = 0f)
    {
        // 1. [방어 코드]: 삭제하려는 대상이 이미 비어있는지 확인합니다.
        if (obj == null)
        {
            return; // 대상이 없으면 아무것도 하지 않고 함수를 종료 (에러 방지)
        }

        // 2. [기능 실행]: 실제 유니티 엔진에 파괴 명령을 전달합니다.
        // t는 '지연 시간'으로, 기본값이 0이라서 넣지 않으면 즉시 파괴됩니다.
        Object.Destroy(obj, t);
    }
}
