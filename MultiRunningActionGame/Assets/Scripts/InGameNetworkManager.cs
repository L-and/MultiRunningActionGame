using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InGameNetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
