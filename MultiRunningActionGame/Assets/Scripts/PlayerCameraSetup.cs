using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 플레이어가 생성됐을때 카메라를 부착해주는 스크립트
public class PlayerCameraSetup : MonoBehaviour
{
    public PhotonView PV;
   private Transform tr;
   void Awake()
   {    
       tr = GetComponent<Transform>();
       if (PV.IsMine)
        Camera.main.GetComponent<CameraAttacher>().targetTransform = tr.Find("CameraPivot").transform;           
   }

}
