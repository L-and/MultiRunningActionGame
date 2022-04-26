using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMoveManager : MonoBehaviour
{
    public PhotonView PV;
    public Transform playerListPivot;
    public Text PlayerTextPrefab; // 플레이어 텍스트 프리팹
    public Text[] PlayerTextList = new Text[PhotonNetwork.PlayerList.Length];

    void Start()
    {
        for(int i=1; i<=PhotonNetwork.PlayerList.Length; i++) // 플레이어의 수만큼 TextUI 생성
        {
            PlayerTextList[i] = Instantiate(PlayerTextPrefab, playerListPivot.position + Vector3.down * 40 * i, Quaternion.identity);
            PlayerTextList[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }
    void Update()
    {
        
    }
}
