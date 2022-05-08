using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObstacleGenerator : MonoBehaviour
{
    public Transform firstSpawnTrnasform; // 첫번째 장애물의 위치

    Vector3 obstacleOffset; // 장애물 사이의 거리 벡터
    public int OffsetDistance; // 장애물 사이의 거리
    public int genCount; // 생성할 장애물의 갯수

    public GameObject[] obstacles; // 장애물 오브젝트 리스트
    private PhotonView pv;


    bool isNeedGen;

    private void Start()
    {
        isNeedGen = true; // 게임시작할때 기본 장애물을 세팅하기위해 기본값을 true 로 설정
        pv = PhotonView.Get(this);
    }

    private void Update()
    {
        tryGenObstacle(); // 장애물 랜덤생성
    }


    void tryGenObstacle()
    {
        if (isNeedGen && PhotonNetwork.IsMasterClient) // 장애물의 생성이 필요 && 마스터클라이언트
            genObstacle();
    }

    void genObstacle()
    {
        
        for (int i = 1; i <= genCount; i++)
        {
            obstacleOffset = new Vector3(OffsetDistance * i, 0, 0);
            int obstacleIndex = Random.Range(0, obstacles.Length); // 생성되는 장애물을 랜덤으로 선택해서
            
            pv.RPC("genObjectRPC", RpcTarget.All, obstacleIndex, firstSpawnTrnasform.position + obstacleOffset); // RPC로 클라이언트마다 그 인덱스에 맞는 장애물을 생성하게 함
        }

        isNeedGen = false; // 장애물을 생성했으니 false 로 설정해줌 
    }

    [PunRPC]
    void genObjectRPC(int obstacleIndex, Vector3 spawnPosition)
    {
        Instantiate(obstacles[obstacleIndex], spawnPosition, Quaternion.identity); // 인스턴스 생성
    }
    
}
