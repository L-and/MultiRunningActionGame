using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayerPosition : MonoBehaviour, IPunObservable
{
    PhotonView PV;

    // 네트워크 동기화로 받을 변수들
    Vector2 networkPosition;
    Vector2 networkVelocity;

    // 렉 해결을위한 변수
    Vector2 lastPacketPosition; // 이전패킷 위치
    Vector2 lastPacketVelocity; // 이전패킷 벨로시티

    double currentPacketTime = 0;
    double lastPacketTime = 0;
    float currentTime = 0;


    Rigidbody2D rigid; // 리지드바디

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        PositionSync();
        //PTVC.SetSynchronizedValues(rigid.velocity, 0.0f);
    }

    void PositionSync()
    {
        if (!PV.IsMine)
        {
            double timeToReachGoal = currentPacketTime - lastPacketTime;
            currentTime += Time.deltaTime;

            rigid.position = Vector2.Lerp(lastPacketPosition, networkPosition, (float)(currentTime / timeToReachGoal));
            rigid.velocity = Vector2.Lerp(lastPacketVelocity, networkVelocity, (float)(currentTime / timeToReachGoal));
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rigid.position);
            stream.SendNext(rigid.velocity);
        }
        else
        {
            networkPosition = (Vector2)stream.ReceiveNext();
            networkVelocity = (Vector2)stream.ReceiveNext();

            // 렉 해결
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime; // 이전 패킷타임을 저장
            currentPacketTime = info.SentServerTime; // 현재 패킷타임을 저장

            lastPacketPosition = rigid.position;
            lastPacketVelocity = rigid.velocity;
        }
    }
}
