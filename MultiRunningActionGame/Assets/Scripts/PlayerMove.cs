using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{

    public PhotonView PV;
    public float speed; // 이동속도
    public float jumpPower; // 점프력

    public float moveDistance; // 이동거리
    float maxSpeed; // 최대속도
    float minSpeed; // 최저속도

    private bool isJump; //점프

    private Rigidbody2D rigid;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!PV.IsMine && PhotonNetwork.IsConnected)
            return;

        Jump();
        UpdateDistance();
        GetInput();
    }

    void GetInput() // 사용자입력받기
    {
        isJump = Input.GetButtonDown("Jump");
    }

    void Jump()
    {
        if(isJump)
        {
            rigid.velocity = Vector2.up * jumpPower;
        }
    }

    void UpdateDistance()
    {
        moveDistance += speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.smoothDeltaTime, 0, 0);
    }
}
