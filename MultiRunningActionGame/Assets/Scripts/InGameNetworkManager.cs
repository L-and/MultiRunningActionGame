using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InGameNetworkManager : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    
    void Awake()
    {
        player = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<InGameUI>().player = player; // 플레이어 객체 지정해줌

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
