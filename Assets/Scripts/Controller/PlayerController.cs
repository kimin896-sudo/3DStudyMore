using System;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 destPosition;
   // bool moveToDest = false;
    Animator animator;

    float wait_run_ratio = 0f;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Jumping,
        Falling
    }
    PlayerState state = PlayerState.Idle;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Managers.Input.MouseAction += OnMouseClicked;

        /*   for (int i = 0; i < 10; i ++)
           {
               UI_Button uiBtn = Managers.UI_Manager.ShowPopUI<UI_Button>("UI_Button");
           }*/
        //Managers.UI_Manager.ClosePopupUI(uiBtn);
        // Managers.UI_Manager.ClosePopupUI();

        // Temp
       /* Managers.UI_Manager.ShowSceneUI<UI_Inven>("UI_Inven");*/
    }
    private void Update()
    {
        switch (state)
        {
            case PlayerState.Die:
                UpDateDie();
                break;
            case PlayerState.Moving:
                UpDateMoving();
                break;
            case PlayerState.Idle:
                UpDateIdle();
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Falling:
                break;
            default:
                break;
        }
    }

    void UpDateDie()
    {
        animator.Play("FootAttack");
    }

    void UpDateMoving()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, destPosition, speed * Time.deltaTime);
        Vector3 dir = destPosition - transform.position;// 목적지로 가는 방향 구하기 

        if (dir.magnitude < 0.01f) // 거리가 0과 가까워지면 
        {
            //  moveToDest = false; // 목적지에 거의 근접했다면 
            state = PlayerState.Idle; // 정지 정지 움직이면 쏜다.
        }
        else
        {
            //transform.position = newPos;
            //  float movedir = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);
            transform.position = newPos;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), speed * Time.deltaTime);
            //  transform.LookAt(destPosition); // 목적지를 향해서 몸을 돌림
        }


        //애니매이션처리 
         wait_run_ratio = Mathf.Lerp(wait_run_ratio, 2, 10f * Time.deltaTime);
        animator.SetFloat("wait_run_ratio", wait_run_ratio);
        // animator.SetBool("Move", true);
         animator.Play("WAIT_RUN");  // 목적지 도착시 멈춤 
    }
    void UpDateIdle()
    {
        if (wait_run_ratio < 0.1f) wait_run_ratio = 0;

        wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 5f * Time.deltaTime);
        animator.SetFloat("wait_run_ratio", wait_run_ratio);
       // animator.SetBool("Move",false);
        // 목적지 도착시 멈춤 
        animator.Play("WAIT_RUN");
    }
    void OnMouseClicked(Define.MouseEvent monuseEvent)
    {
        if (state == PlayerState.Die)
        {
            return;
        }

        // 클릭이면 종료 
        if (monuseEvent == Define.MouseEvent.Click)
        {
            return;
        }
        //Loger.Log($"OnMouseClick {this.name}");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 2);

        // 이게 가장 빠름 
        int layerMask = (1 << Define.CLICK_LAYER);

        // (1 << 8) | (1 << 9)

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            //Loger.Log($"raycast camera{hit.collider.gameObject.name}");

            destPosition = hit.point;
            state = PlayerState.Moving;
        }
    }
}

