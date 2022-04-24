using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCameraSetup : MonoBehaviour
{
    public PhotonView PV;
   private Transform tr;
   void Start()
   {
       tr = GetComponent<Transform>();
       if (PV.IsMine)
           Camera.main.GetComponent<CameraAttacher>().targetTransform = tr;
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
