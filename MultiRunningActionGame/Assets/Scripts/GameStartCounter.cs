using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
// 씬이 변경된후 경과한시간을 측정해서 시작카운트를 함
public class GameStartCounter : MonoBehaviour
{
    public PhotonView PV;

    public GameObject countText; // 카운트를 출력하는 UI
    public float offsetSecond; // 카운트가 출력되기까지 여유시간

    private float currentSecond;

    private GameObject[] players; // 플레이어 객체를 담을 배열

    void Awake()
    {
        countText.GetComponent<Text>().text = "Ready..."; // 텍스트 초기화

        players = new GameObject[PhotonNetwork.PlayerList.Length];
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
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            players[i].GetComponent<PlayerController>().enabled = true;
            players[i].GetComponent<SyncPlayerPosition>().enabled = true;
        }
    }
}