using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleManager : MonoBehaviour
{
    GameObject[] players; // player들의 distance를 얻기위한 변수
    float[] movedistanceList;
    bool coroutineCheck;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        movedistanceList = new float[players.Length];
        coroutineCheck = true;
    }

    private void Update()
    {
        if (coroutineCheck)
        {
            coroutineCheck = false;
            StartCoroutine(updateMoveDistanceCoroutine()); // 매번 호출하면 퍼포먼스가 떨어질거같아서 일정시간마다 코루틴으로 호출
        }
  
    }

    IEnumerator updateMoveDistanceCoroutine()
    {
        for (int i = 0; i < players.Length; i++)
        {
            movedistanceList[i] = players[i].GetComponent<PlayerController>().moveDistance; // 컨트롤러에서 현재 지나온 거리를 가져와서 movedistanceList에 저장
            print(players[i].name + ":" + movedistanceList[i]);
        }

        yield return new WaitForSecondsRealtime(2.0f);
        coroutineCheck = true;
    }
}
