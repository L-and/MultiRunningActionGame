using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncPlayerPosition : MonoBehaviour, IPunObservable
{
    PhotonView PV;
    Vector3 curPos; // 직접 통신을 해서 위치를 받을 벡터

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private void Update()
    {
        PositionSync();
    }

    void PositionSync()
    {
        if (PV.IsMine)
        { }
        else if ((transform.position - curPos).sqrMagnitude >= 100) // 전송받은 위치가 너무 멀다면 텔레포트
            transform.position = curPos;
        else
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }

        //클론이 통신을 받는 
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
}
