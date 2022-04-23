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
	public Text userCountText;

	void Start() => PhotonNetwork.ConnectUsingSettings();

	public override void OnConnectedToMaster() => 
		PhotonNetwork.JoinLobby();

	public override void OnJoinedLobby() => 
		PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 4 }, null);

	public override void OnJoinedRoom() =>
		PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);

	void Update()
	{
		statusText.text = PhotonNetwork.NetworkClientState.ToString();
		
		userCountText.text = PhotonNetwork.CountOfPlayers.ToString() + '/' + PhotonNetwork.CountOfPlayersOnMaster.ToString();
	}
}