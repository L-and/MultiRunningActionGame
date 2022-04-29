using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public byte maxPlayerCnt;

    public GameObject mainUIObj;
    public Text networkStats;
    public InputField nickNameInput;

    public GameObject lobbyUIObj;
    public GameObject readyButtonObj;
    public GameObject startButtonObj;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        Connect(); //서버에 접속
    }

    private void Update()
    {
        networkStats.text = PhotonNetwork.NetworkClientState.ToString(); // 네트워크상태 UI로 표시
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public void CreateorJoinRoom() // 방을 생성하거나 방이 있으면 참가
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = maxPlayerCnt }, null);
        PhotonNetwork.LocalPlayer.NickName = nickNameInput.text; // 닉네임 설정
    }

    public override void OnConnectedToMaster() // 마스터접속시 콜백
    {
        print("[마스터]접속완료");
        PhotonNetwork.JoinLobby(); // 로비에 접속
    }

    public override void OnJoinedLobby() // 로비접속시 콜백
    {
        print("[로비]접속완료");
    }

    public override void OnJoinedRoom() // 룸접속시 콜백
    {
        print("[방]접속완료");
        mainUIObj.SetActive(false); // 메인UI를 꺼줌
        lobbyUIObj.SetActive(true); // 로비UI를 켜줌
        ButtonActivator();
    }

    public void ButtonActivator()
    {
        print("클라이언트 상태" + PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
            startButtonObj.SetActive(true); // 마스터클라이언트면 시작버튼을 활성화
        else
            readyButtonObj.SetActive(true); // 일반클라이언트면 준비버튼을 활성화
            
    }
}
