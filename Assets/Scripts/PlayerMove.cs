using System.Collections;
using TMPro;
using TreeEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMove : MonoBehaviour
{
    GameObject go;

    public float _moveSpeed = 5;
    public float _turnSpeed = 5;
    public float _rayLength = 100f;

    public GameObject _Marker;

    CharacterController charactercontroller;

    //Animation anim;
    /*    // 그림을 기려줌 
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.forward * _rayLength);
        }*/

    // navamsh = 내부에 길찾기 알고리즘 생성 
    //Animation anim;
    Animator animator;
    void Start()
    {
        //anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        _layerMask = (1 << Define.CLICK_LAYER);

        charactercontroller = GetComponent<CharacterController>();
    }
    // 해당하는 지점까지의 걸리게될 시간을 예측 
    // 실제 움직이는데 그 시간까지 움직이지 못했다면 정지를 시킴
    float _est;// 목표지점까지 걸리는 시간을 계산에 사용하는 변수 
    float _elapsedTime = 0; // 경과시간을 계산하는 변수 






    // 비트         00000000000011000000000000
    //int layerMask = (1 << 8) + (1 << 9);
    // 숫자 이름화
    //int layerMask = (1 << 9);
    int _layerMask;

    bool isMoving = false;

    //1. 불리언으로 상태를 관리 
    //2. 매 프레임당 이동할 거리를 구하고
    //3. 목표 지점까지 달리고 
    //4. 목표 지점에 닿으면 쉬기 
    bool isMoveState = false;

    public enum PlayerState
    {
        Idle = 0,
        Run = 1
    }

    public PlayerState state;

    // 활성화가 되었을 때 실행 SetEnable Awake 다음 바로 실행 
    private void OnEnable()
    {

    }

    Vector3 mousePosition;
    void Update()
    {
        if (isMoveState) // 입력을 통해 움직이는 부분 
        {
            // 시간 누적 
            _elapsedTime += Time.deltaTime;
            // 벽, 장애물 등에 막혀서 이동시간이 지연될 경우 멈춘다.
            // 이동하는 데 걸린 실제 시간 > 이동하는데 걸려야 하는 예상 시간
            if (_elapsedTime > _est)
            {
                isMoveState = false;
                animator.SetInteger("State", (int)PlayerState.Idle);
                return;
            }
            Vector3 targetPosition = mousePosition;

            targetPosition.y = transform.position.y;

            // 거리 계산 
            Vector3 framePos = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            // 이동
            Vector3 moveDir = framePos - transform.position;
            // 캐릭터 이동 
            charactercontroller.Move(moveDir);
            // 캐릭터 회전 
            transform.forward = Vector3.Slerp(transform.forward, moveDir.normalized, Time.deltaTime * _turnSpeed);
            // 목표 지점 도착 쉬기 
            //- 도착했는지

            // 도착했을 때
            if (framePos == targetPosition)
            {
                isMoving = false;
                animator.SetInteger("State", 0);
                //  _Marker.SetActive(false);
            }
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); // 스크린 좌표를 레이로 

            // 레이저를 쐈을때 맞았으면 true 안 맞으면 false ||l layermask가 들어간 얘들만 출력  
            if (Physics.Raycast(r, out hitInfo, _rayLength, _layerMask))
            {

                mousePosition = hitInfo.point;
                /* if (go == null)
                 {
                     go = Instantiate(_Marker);
                     go.name = "@Maker";
                 }*/
                // go = Instantiate(_Marker);
                //  go.name = "@Maker";
                // go.transform.position = hitInfo.point;
                isMoveState = true;
                // go.SetActive(true);

                /*  Instantiate(_Marker);
                  _Marker.transform.position = hitInfo.point;*/
                //Loger.Log($"picked{hitInfo.transform.name}");

                /*    direction = (hitInfo.point - transform.position);
                    direction.y = 0; // 높이 차이 무시 (바닥 이동용)
                    if (direction.magnitude > 0.1f) // 목적지에 거의 도착했는지 확인
                    {
                        // 캐릭터가 목적지를 바라보게 함
                        transform.forward = Vector3.Slerp(transform.forward, -direction.normalized, Time.deltaTime * 10f);
                        // 이동 실행
                        charactercontroller.Move(direction.normalized * moveSpeed * Time.deltaTime);
                    }*/
                // 목표지점까지 이동에 걸리는 시간을 계산 
                _est = Vector3.Distance(mousePosition, transform.position) / _moveSpeed + 1f; // 거리를 계산하는 함수 
                _elapsedTime = 0;
                animator.SetInteger("State", 1);
            }
            else
            {
                Loger.Log("안 맞음");
            }

        }
        /*  RaycastHit hitInfo;
          if(Physics.Raycast(transform.position,-transform.forward,out hitInfo, rayLength))
          {
              // point : 감지된 위치 
              // normal : 법선 백터( 반사각을 구할때 사용)
              // hitinfo.distance  : ray가 감지한 거리 

              Loger.LogError($"Dectected GameObject : {hitInfo.point}");
          }*/
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        // charactercontroller.Move((Vector3.forward * v) * _moveSpeed * Time.deltaTime);
        //   transform.Rotate(0, _turnSpeed * Time.deltaTime * h, 0);

    }
}
