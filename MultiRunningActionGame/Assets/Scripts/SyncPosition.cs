using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
 
public class SyncPosition : MonoBehaviourPunCallbacks, IPunObservable {
 
    public float speed = 10.0f;
    private Transform tr;
 
    void Start()
    {
        tr = GetComponent<Transform>();
    }
 
    void Update () {
        //controlled locally일 경우 이동(자기 자신의 캐릭터일 때)
        if (!photonView.IsMine)
        {
            //끊어진 시간이 너무 길 경우(텔레포트)
            // if ((tr.position - currPos).sqrMagnitude >= 10.0f * 10.0f)
            // {
            //     tr.position = currPos;
            //     tr.rotation = currRot;
            // }
            //끊어진 시간이 짧을 경우(자연스럽게 연결 - 데드레커닝)
            // else
            // {
                tr.position = currPos;
            // }
        }
 
    }
 
    //클론이 통신을 받는 변수 설정
    private Vector3 currPos;
 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
        }
 
        //클론이 통신을 받는 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
        }
    }
}