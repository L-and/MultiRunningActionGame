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
        cameraActivate();
    }

    private void cameraActivate() // 카메라 활성화
    {
        if (!PV.IsMine)
            playerCamera.SetActive(false);
    }
}
