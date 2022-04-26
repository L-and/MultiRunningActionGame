using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 플레이어의 스폰을 담당하는 스크립트
public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    private GameObject player;
    
    private void Start() 
    {
        Debug.Log("플레이어 생성!");
		player = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
        player.name = "Player " + player.GetComponent<PhotonView>().Owner.NickName;
    }
}
