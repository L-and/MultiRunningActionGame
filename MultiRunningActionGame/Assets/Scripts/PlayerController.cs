using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 플레이어의 이동을 담당하는 스크립트
public class PlayerController : MonoBehaviour
{

    public PhotonView PV;

    public float jumpPower; // 점프력
    public bool isJump; // 현재 점프중인가?
    
    public float speed; // 이동속도
    public float moveDistance; // 이동거리
    float maxSpeed; // 최대속도
    float minSpeed; // 최저속도

    private bool isJumpInput; //점프입력
    private float horizontalInput;

    private Rigidbody2D rigid;
    private Transform tr;

    Vector3 curPos; // 직접 통신을 해서 위치를 받을 벡터

    void Start()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PV.IsMine) // 현재 클라이언트 플레이어라면 실행
        {
            GetInput();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (PV.IsMine) // 현재 클라이언트 플레이어라면 실행
        {

            UpdateDistance();
            Move();
            //if (horizontalInput != 0.0f)
            //transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;

        }


    }

    void GetInput() // 사용자입력받기
    {
        isJumpInput = (Input.GetTouch(0).phase ==  TouchPhase.Began);
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void Jump()
    {
        if (isJumpInput && !isJump) // 점프키가 눌리고 점프중이 아닐때
        {
            rigid.velocity = Vector2.up * jumpPower; // 점프
            isJump = true; // 현재 점프중으로 변경
        }
    }

    void UpdateDistance()
    {
        moveDistance += speed * Time.deltaTime;
    }

    void Move()
    {
            rigid.position += Vector2.right * speed * Time.fixedDeltaTime;
    }
}