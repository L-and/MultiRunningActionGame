using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]

public class CountSync : MonoBehaviourPunCallbacks, IPunObservable
{
    public float currentSecond;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentSecond);
        }
        else if(stream.IsReading)
        {
            currentSecond = (float)stream.ReceiveNext();
        }
    }

    void Update()
    {
        currentSecond += Time.deltaTime;
    }

    public float GetCurrentSecond()
    {
        return currentSecond;
    }
}
