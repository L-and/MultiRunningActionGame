using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    /// 플레이어관련 변수들 ///
    public GameObject playerPrefab; // 플레이어 프리팹
    public Transform playerSpawnTransform;
    public void PlayerSpawn()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnTransform.position, Quaternion.identity); // 플레이어 프리팹 생성
    }

}
