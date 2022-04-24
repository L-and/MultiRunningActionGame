using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	string networkState;
	public Text statusText;
	public Text roomNameText;

	void Update() => statusText.text = PhotonNetwork.NetworkClientState.ToString(); // 연결상태 업데이트

	void Start() => PhotonNetwork.ConnectUsingSettings();

	public void joinLobby() => PhotonNetwork.JoinLobby();

	public void CreateRoom() => PhotonNetwork.CreateRoom(roomNameText.text);

	public void joinRoom() => PhotonNetwork.JoinRoom(roomNameText.text);

	public override void OnConnectedToMaster() 
	{
		Debug.Log("마스터서버접속 완료");
	}

	public override void OnCreatedRoom() => 
		Debug.Log("방생성 완료");

	public override void OnJoinedRoom() =>
		Debug.Log("방접속 완료");
		
}