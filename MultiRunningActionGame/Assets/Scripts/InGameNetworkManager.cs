using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InGameNetworkManager : MonoBehaviour
{
    public Transform spawnPoint;
    void Awake()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
    }

    public void PlayerSpawn()
    {
        PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
