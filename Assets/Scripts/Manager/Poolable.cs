using UnityEngine;

// 풀매니저에서 풀 할 오브젝트 구별을 위해 
// 이 스크립트가 붙어있다면 풀 할 오브젝트로 지정
public class Poolable : MonoBehaviour
{
    public bool IsUsing;  // 현재 오브젝트가 사용하고 있나 없나 확인 
}
