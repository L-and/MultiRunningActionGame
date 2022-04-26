using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun ;
using Photon.Realtime;

// 멀티플레이시 움직임이 튀는걸 막아주는 스크립트(제작중)
public class SyncPosition : MonoBehaviourPunCallbacks, IPunObservable 
{
    public float SmoothingDelay = 5;
    private Transform tr;
    public PhotonView PV;
    public Vector3 networkPosition;
    public float movementSpeed;
    private Vector2 movement;

    void Start() 
    {
        tr = GetComponent<Transform>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 클론이 정보를 보냄
        if (stream.IsWriting)
        {
            if(PV.IsMine)
                stream.SendNext(tr.position);
                Debug.Log("정보보냄");
        }// 클론이 정보를 받음
        else if(stream.IsReading)
        {
            if(PV.IsMine)
                networkPosition = (Vector3)stream.ReceiveNext();
                Debug.Log(networkPosition);
        }
    }



    // Update is called once per frame
    void Update()
    {   
        if (!PV.IsMine) // 플레이중인 객체가 아니라면 이동보정을 해줘서 떨리는걸 막아줌
        {
            // transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * this.SmoothingDelay);
            // Debug.Log(this.transform.position);
        }
    }
}
