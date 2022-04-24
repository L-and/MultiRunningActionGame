using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed; // 이동속도
    public float jumpPower; // 점프력
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
        Jump();

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

    void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
