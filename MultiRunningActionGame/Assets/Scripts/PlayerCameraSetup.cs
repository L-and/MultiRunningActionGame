using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCameraSetup : MonoBehaviour
{
    public PhotonView PV;
   private Transform tr;
   private float speed;

   private Transform cameraTransform;
   void Start()
   {    
       speed = gameObject.GetComponent<PlayerMove>().speed;

       tr = GetComponent<Transform>();
       if (PV.IsMine)
           cameraTransform = GameObject.FindWithTag("InGameCamera").GetComponent<Transform>();
           
   }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraTransform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
