using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 플레이어의 이동을 담당하는 스크립트
public class PlayerController : MonoBehaviour, IPunObservable
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

    Vector3 curPos; // 직접 통신을 해서 위치를 받을 벡터

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!PV.IsMine && PhotonNetwork.IsConnected) // 로컬 플레이어가아니면 실행X
            return;

        PV.RPC("JumpRPC", RpcTarget.All);
        UpdateDistance();
        GetInput();

        
    }

    void GetInput() // 사용자입력받기
    {
        isJumpInput = Input.GetButtonDown("Jump");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    [PunRPC]
    void JumpRPC()
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

    void PositionSync()
    {
        if(!PV.IsMine)
        {

        }
    }

    void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (!PV.IsMine) // 로컬 플레이어가아니면 실행X
            return;

        //Move();
        if (horizontalInput != 0.0f)
            transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    { 
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }


}