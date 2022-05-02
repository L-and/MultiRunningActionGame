using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 카메라를 플레이어에게 붙여주는 스크립트
public class CameraAttacher : MonoBehaviour
{
    public Transform targetTransform;
    PhotonView playerPv;

    GameObject[] playerList;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position; // 카메라위치로 offset 설정

        setTargetTransform(); //targetTransform을 플레이어로 설정
    }

    void FixedUpdate()
    {
        Vector3 targetVector = targetTransform.position + offset;

        gameObject.transform.position = new Vector3(0, targetVector.y, targetVector.z); // x축이동을 고정한채로 카메라움직임
    }

    private void setTargetTransform()
    {
        if (targetTransform != null) // 타겟이 정해져있으면 실행X
            return;

        playerList = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playerList.Length; i++)
        {
            playerPv = playerList[i].GetComponent<PhotonView>();

            if (playerPv.IsMine)
            {
                targetTransform = playerList[i].transform;
            }
        }
    }
}