using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RankingSystem : MonoBehaviour
{

    public GameObject[] players; // player들의 distance를 얻기위한 변수


    public List<float> movedistanceList; // 다른 플레이어들의 distance들

    public float dieDistance; // 사망거리

    public GameObject uiCamera; // UI카메라

    public GameObject winUI; // 승리화면
    public GameObject loseUI; // 탈락화면


    public GameObject rankingListStartPosition; // 랭킹리스트 초기위치

    public PhotonView PV;

    private void Awake()
    {

        players = new GameObject[PhotonNetwork.PlayerList.Length];
        players = GameObject.FindGameObjectsWithTag("Player"); // 플레이어 객체 할당

        for (int index = 0; index < PhotonNetwork.PlayerList.Length; index++) // movedistanceList의 모든 객체를
        {
            movedistanceList.Add(players[index].transform.position.x); // 이동거리를 설정해줌
        }

    }

    private void Update()
    {
        updateMoveDistanceList(); // moveDistanceList 업데이트
        loseCheck();
        winCheck();
    }


    private void updateMoveDistanceList()
    {
        for (int index = 0; index < PhotonNetwork.PlayerList.Length; index++) // movedistanceList의 모든 객체를
        {
            if(players[index] != null)
            movedistanceList[index] = players[index].transform.position.x; // 이동거리를 설정해줌
        }

        movedistanceList.Sort(compare); // 내림차순으로 정렬
    }

    static int compare(float c1, float c2)
    {
        if (c1 < c2) return 1;
        if (c1 > c2) return -1;
        return 0;
    }

    private void loseCheck()
    {
        for(int index=0; index<players.Length; index++)
        {
            float moveDistance;
            if (players[index] != null)
                moveDistance = players[index].GetComponent<PlayerController>().moveDistance;
            else
                moveDistance = 0.0f;
            
            if(moveDistance < movedistanceList[0] - dieDistance && moveDistance != 0.0f) // 1등플레이어와 dieDistance만큼 거리가 멀어지면 탈락처리
            {
                Destroy(players[index]); // 오브젝트 파괴
                PhotonNetwork.Disconnect(); // 접속해제
                loseUI.SetActive(true);
                uiCamera.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }

    private void winCheck()
    {
        if(PV.IsMine && PhotonNetwork.PlayerList.Length == 1)
        {
            winUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
