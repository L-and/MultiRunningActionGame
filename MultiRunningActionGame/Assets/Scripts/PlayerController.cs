using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 플레이어의 이동을 담당하는 스크립트
public class PlayerController : MonoBehaviour
{

    public PhotonView PV;
    public float speed; // 이동속도
    public float jumpPower; // 점프력

    public float moveDistance; // 이동거리
    float maxSpeed; // 최대속도
    float minSpeed; // 최저속도

    private bool isJump; //점프
    private float horizontalInput;

    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!PV.IsMine && PhotonNetwork.IsConnected) // 로컬 플레이어가아니면 실행X
            return;

        Jump();
        UpdateDistance();
        GetInput();
    }

    void GetInput() // 사용자입력받기
    {
        isJump = Input.GetButtonDown("Jump");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void Jump()
    {
        if (isJump)
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    void UpdateDistance()
    {
        moveDistance += speed * Time.deltaTime;
    }

    void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (!PV.IsMine) // 로컬 플레이어가아니면 실행X
            return;

        Move();
        // if(horizontalInput != 0.0f)
        // transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;
    }
}