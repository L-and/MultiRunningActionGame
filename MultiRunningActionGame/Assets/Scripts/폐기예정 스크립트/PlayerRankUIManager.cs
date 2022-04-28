using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerRankUIManager : MonoBehaviour
{
    public PhotonView PV;
    public GameObject inagmaeUI; // UI 게임오브젝트
    public Text playerTextPrefab; // 플레이어 텍스트 프리팹
    public Text[] playerTextList;

    void Awake()
    {
        playerTextList = new Text[PhotonNetwork.PlayerList.Length];

        for(int i=0; i<PhotonNetwork.PlayerList.Length; i++) // 플레이어의 수만큼 TextUI 생성
        {
            playerTextList[i] = Instantiate(playerTextPrefab, new Vector3(240, 225 - 40 * i, 0), Quaternion.identity);
            playerTextList[i].text = PhotonNetwork.PlayerList[i].NickName;
            playerTextList[i].transform.SetParent(inagmaeUI.transform, false); // UI오브젝트의 자식으로 설정해줌
        }
    }
    void Update()
    {
    }
}
