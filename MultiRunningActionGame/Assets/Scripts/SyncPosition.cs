using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun ;

public class SyncPosition : MonoBehaviour, IPunObservable
{
    public PhotonView PV;
    public Vector2 networkPosition;
    public float movementSpeed;
    private Vector2 movement;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(networkPosition);
        }
        else
        {
            networkPosition = (Vector2) stream.ReceiveNext();
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {   
        Vector3 oldPosition = transform.position;

        movement = transform.position - oldPosition;
        if (!PV.IsMine) // 플레이중인 객체가 아니라면 이동보정을 해줘서 떨리는걸 막아줌
        {
            transform.position = Vector3.MoveTowards(transform.position, movement, Time.deltaTime * movementSpeed);
        }
    }
}
