using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun ;

// 멀티플레이시 움직임이 튀는걸 막아주는 스크립트(제작중)
public class SyncPosition : MonoBehaviour, IPunObservable
{
    public PhotonView PV;
    public Vector2 networkPosition;
    public float movementSpeed;
    private Vector2 movement;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Vector3 oldPosition = transform.position;

        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading)
        {
            transform.position = (Vector2) stream.ReceiveNext();
        }

        movement = transform.position - oldPosition;
    }



    // Update is called once per frame
    void FixedUpdate()
    {   
        if (!PV.IsMine) // 플레이중인 객체가 아니라면 이동보정을 해줘서 떨리는걸 막아줌
        {
            // transform.position = Vector2.MoveTowards(transform.position, networkPosition, Time.deltaTime * movementSpeed);
            Debug.Log(transform.position);
        }
    }
}
