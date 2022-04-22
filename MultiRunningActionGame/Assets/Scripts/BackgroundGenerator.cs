    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    public GameObject background; // 배경이미지
    Vector3 backgroundOffset; // 배경이 생성되는 위치 오프셋
    GameObject newBackground; // 새로생성된 배경
    int lastIndex;
    int currentIndex;
    float moveDistance; // 캐릭터의 이동거리 (11.35가 최대)
    int backgroundGenCount; // 배경이 생성된횟수(이 값을 보스스테이지 진입기준으로 할 예정)
    void Start()
    {
        backgroundOffset = background.transform.position - player.transform.position;
        lastIndex = 0;
        currentIndex = 1;
    }

    void Update()
    {   

        moveDistance += gameObject.GetComponent<PlayerMove>().speed * Time.deltaTime;
        
        if(moveDistance >= 11.35f) // 배경이 끝나가면
        {
            GameObject newBackground = Instantiate(background, player.transform.position + backgroundOffset, Quaternion.identity); // 플레이어의 시야앞에 새로운 배경을 생성
            moveDistance = 0;
            backgroundGenCount++;
        }
    }
}
