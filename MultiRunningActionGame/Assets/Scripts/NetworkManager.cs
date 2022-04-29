using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviour
{
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }


}
