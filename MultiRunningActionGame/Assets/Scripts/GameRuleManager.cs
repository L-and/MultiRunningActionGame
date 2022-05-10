using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameRuleManager : MonoBehaviour, IPunObservable
{

    GameObject[] players; // player들의 distance를 얻기위한 변수

    float clientMoveDistance; // 내 캐릭터의 distance
    float[] otherMovedistanceList; // 다른 플레이어들의 distance들

    public float updateLate; // distance 업데이트 주기

    bool coroutineCheck; // 코루틴에 들어가도되는지 검사하는 변수

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); // 플레이어 객체 할당

        otherMovedistanceList = new float[players.Length - 1];

        coroutineCheck = true;
    }

    private void Update()
    {
        tryUpdateMoveDistance();
    }

    private void tryUpdateMoveDistance() // 플레이어들의 MoveDistance를 업데이트 시도
    {
        if(coroutineCheck) // 코루틴을 실행할 수 있으면
        {
            coroutineCheck = false;

            StartCoroutine(updateMoveDistanceCoroutine());
        }
    }

    IEnumerator updateMoveDistanceCoroutine()
    {
        print(otherMovedistanceList);
        yield return new WaitForSeconds(updateLate);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(clientMoveDistance);
        }

        //클론이 통신을 받는 
        else
        {
            otherMovedistanceList[info.photonView.ViewID / 1000 - 1] = (float)stream.ReceiveNext();
        }
    }
}
