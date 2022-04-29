using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraActivator : MonoBehaviour
{
    public PhotonView PV;
    public GameObject playerCamera;

    void Awake()
    {
        if (!PV.IsMine)
            playerCamera.SetActive(false);
    }
}
