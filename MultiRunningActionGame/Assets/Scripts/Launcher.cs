using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public byte maxPlayerCnt;

    /// 카메라관련 변수들 ///
    public GameObject mainCamera;
    private CameraAttacher cameraAttacher;

    /// UI관련 변수들 ///
    public GameObject panelObj;

    public GameObject mainUIObj;
    public Text networkStats;
    public InputField nickNameInput;

    public GameObject lobbyUIObj; // 
    public GameObject readyButtonObj; // 룸 준비버튼
    public GameObject startButtonObj; // 룸 시작버튼



    /// 플레이어관련 변수들 ///
    public GameObject playerPrefab; // 플레이어 프리팹
    public Transform playerSpawnTransform;
    GameObject player;

    /// 매니저오브젝트들 ///
    public GameObject gameStartCounter;

    int readyCount; // 레디한 유저의 수

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        // 객체지정
        cameraAttacher = mainCamera.GetComponent<CameraAttacher>();

        Connect(); //서버에 접속
        readyCount = 0;
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
        RoomButtonActivator(); // 클라이언트에따라 준비, 시작버튼을 활성화해줌

        PhotonNetwork.CurrentRoom.CustomProperties.Add("readyCount", readyCount); // readyCount를 커스텀프로퍼티에 추가
    }

    public void RoomButtonActivator()
    {
        print("클라이언트 상태" + PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.IsMasterClient)
            startButtonObj.SetActive(true); // 마스터클라이언트면 시작버튼을 활성화
        else
            readyButtonObj.SetActive(true); // 일반클라이언트면 준비버튼을 활성화
    }

    public void Ready()
    {
        Hashtable readyCountHashTable = new Hashtable(); // 임시로 해시테이블 생성

        int roomReadyCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["readyCount"]; // 서버에 저장된 readyCount값을 받아옴
        readyCountHashTable.Add("readyCount", roomReadyCount + 1); // 해시테이블에 readyCount + 1 을 해서 저장함

        PhotonNetwork.CurrentRoom.SetCustomProperties(readyCountHashTable); // 새로 해시테이블을 Set해줌

        readyButtonObj.SetActive(false); // 준비후 준비버튼 비활성화
    }

    public void GameStart()
    {
        PV.RPC("GameStartRPC", RpcTarget.All);
    }

    [PunRPC]
    public void GameStartRPC()
    {
        int currentReadyCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["readyCount"] + 1;
        bool startReady = (PhotonNetwork.CurrentRoom.Players.Count == currentReadyCount);
        if (startReady)
        // 방장을 제외한 모두가 준비됐다면
        {
            print("게임시작!");
            panelObj.SetActive(false); // UI패널 비활성화
            lobbyUIObj.SetActive(false); // 로비UI 비활성화
        }
        else
        {
            print("현재 레디한인원:" + currentReadyCount);
        }

        player = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnTransform.position, Quaternion.identity); // 플레이어인스턴스 생성
        player.name = "Player["+PhotonNetwork.LocalPlayer.NickName+"]"; // 플레이어이름설정

        cameraAttacher.enabled = true; // 카메라가 플레이어에게 붙도록 설정

        gameStartCounter.SetActive(true);
    }
}
