using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	string networkState;

	void Start() => PhotonNetwork.ConnectUsingSettings();

	public override void OnConnectedToMaster() => 
		PhotonNetwork.JoinLobby();

	public override void OnJoinedLobby() => 
		PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 4 }, null);

	void Update()
	{
		string curNetworkState = PhotonNetwork.NetworkClientState.ToString();
		if (networkState != curNetworkState) 
		{
			networkState = curNetworkState;
			print(networkState);
		}
	}
}