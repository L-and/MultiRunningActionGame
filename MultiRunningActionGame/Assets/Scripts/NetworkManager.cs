using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

// 멀티플레이 접속을 관리해주는 스크립트
public class NetworkManager : MonoBehaviourPunCallbacks
{
	public byte maxPlayers;

	public InputField nickNameInput;
	public PhotonView PV;

	public GameObject player;
    public Transform spawnPoint;


	void Start()
	{
		PhotonNetwork.ConnectUsingSettings(); // 마스터서버에 접속
		PV = photonView;
	}


	void Update()
	{
		
	}

	

	public void JoinRoom()
	{
		PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = maxPlayers }, null); // 방생성
	}



	public override void OnConnectedToMaster() => Debug.Log("마스터서버접속 완료");

	public override void OnCreatedRoom() => Debug.Log("방생성 완료");

	public override void OnJoinedRoom() // 방에 접속하면 닉네임설정
	{
		Debug.Log("방접속 완료");
		PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;
	}

	public void GameStart()
	{
		if (PV.IsMine && PhotonNetwork.InRoom) // 방장이고 방에 입장해있다면
		{
			PV.RPC("StartRPC", RpcTarget.All); // RPC함수 호출

		}

	}
	public void PlayerSpawn()
	{
		GameObject.Find("PlayerSpawner").SetActive(true);
	}


	[PunRPC]
	void StartRPC() // 게임시작
	{
		StartCoroutine("StartMainSceenCorutine");
	}

	IEnumerator StartMainSceenCorutine()
	{
		PhotonNetwork.IsMessageQueueRunning = false;
		PhotonNetwork.LoadLevel("Main"); // Main씬을 로드

		yield return 2;
	}

	



	[ContextMenu("정보")]
	void Info()
	{
		if (PhotonNetwork.InRoom)
		{
			print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
			print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
			print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

			string playerStr = "방에 있는 플레이어 목록 : ";
			for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
			print(playerStr);

		}
		else
		{
			print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
			print("방 개수 : " + PhotonNetwork.CountOfRooms);
			print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
			print("로비에 있는지? : " + PhotonNetwork.InLobby);
			print("연결됐는지? : " + PhotonNetwork.IsConnected);
		}
	}
}
