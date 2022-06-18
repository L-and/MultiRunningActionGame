using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
// 씬이 변경된후 경과한시간을 측정해서 시작카운트를 함
public class GameStartCounter : MonoBehaviour
{
    public PhotonView PV;
    PhotonView playerPV;

    public GameObject countText; // 카운트를 출력하는 UI
    public float offsetSecond; // 카운트가 출력되기까지 여유시간

    private float currentSecond;

    private GameObject[] players; // 플레이어 객체를 담을 배열

    public GameObject gameRuleManager;

    bool isRpcCalled; // rpc를 한번호출했으면 실행안되게 해줌

    void Awake()
    {
        countText.GetComponent<Text>().text = "Ready..."; // 텍스트 초기화

        players = new GameObject[PhotonNetwork.PlayerList.Length];

        isRpcCalled = false;
    }

    void Update()
    {
        currentSecond += Time.deltaTime; //경과시간 업데이트

        StartCount();
    }

    void StartCount()
    {
        if (currentSecond >= 5.5f + offsetSecond) // 카운트UI 안보이게해줌
        {
            countText.GetComponent<Text>().text = "";
        }
        else if (currentSecond >= 4.0f + offsetSecond)
        {
            Debug.Log("Count:Start!");
            countText.GetComponent<Text>().text = "Start!";

            PV.RPC("OnPlayerControllerRPC", RpcTarget.All);
            // 모든 Player 태그가붙은 객체의 PlayerController컴포넌트를 활성화
        }
        else if (currentSecond >= 3.0f + offsetSecond)
        {
            Debug.Log("Count:1");
            countText.GetComponent<Text>().text = "1";
        }
        else if (currentSecond >= 2.0f + offsetSecond)
        {
            Debug.Log("Count:2");
            countText.GetComponent<Text>().text = "2";
        }
        else if (currentSecond >= 1.0f + offsetSecond)
        {
            Debug.Log("Count:3");
            countText.GetComponent<Text>().text = "3";
        }
    }
    
    [PunRPC]
    public void OnPlayerControllerRPC()
    {
        if (isRpcCalled) // 이미 rpc를 호출했으면 실행안되게 해줌
            return;
        else
            isRpcCalled = true;

        players = GameObject.FindGameObjectsWithTag("Player");

        for(int i=0; i<players.Length; i++)
            print(players[i].name);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (players[i].GetPhotonView().IsMine) // 현재 클라이언트만 자동이동 활성화(그래야 끊기는게 없음)
            {
                players[i].GetComponent<PlayerController>().enabled = true; // 플레이어컨트롤러 활성화
            }

            players[i].GetComponent<SyncPlayerPosition>().enabled = true; // 위치동기화 활성화

            if(gameRuleManager != null)
                gameRuleManager.SetActive(true); // 게임룰매니저 활성화
        }
    }
}