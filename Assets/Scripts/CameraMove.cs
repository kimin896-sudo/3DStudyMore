using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //cinemachine

    // 카메라가 따라다닐 캐릭터의 트렌스폼
    public Transform target;
    Vector3 targetDir;

    // 부드럽게 따라다닐 시간.
    public float smoothTime = 0.01f;
    // SmoothDamp 함수에 사용될 함수 
    Vector3 velocity;

    void Start()
    {
       // Vector3.SmoothDamp(현재위치,새위치,ref 현재속도,지연시간); // 
        // 캐릭터에서 카메라의 위위치로 향하는 방향 벡터 구하기
        targetDir = transform.position - target.transform.position ;
    }

    void Update()
    {

        // 캐릭터의 새로운 위치에 카메라의 방향 벡터를 더해서 새로운 위치를 구한다. 
        Vector3 newPosition = target.transform.position + targetDir;
        // 현재는 플레이어에 고정된 상태 
        //transform.position = newPosition;

        //Vector3.smoothDamp()를 이용해서 부드럽게 캐릭터를 따라다니도록 설정 
        // smoothTime 만큼 시간차를 두고 이동 
        transform.position = Vector3.SmoothDamp(transform.position, newPosition,ref velocity,smoothTime);



    }
}
