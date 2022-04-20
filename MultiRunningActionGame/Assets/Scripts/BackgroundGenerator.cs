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
    void Start()
    {
        backgroundOffset = background.transform.position - player.transform.position;
        lastIndex = 0;
        currentIndex = 1;
    }

    void Update()
    {   
        if((int)player.transform.position.x % 12f == 0) // 캐릭터가 일정거리 이동하고 새로운 맵이 없을떄
        {
            currentIndex++;
            GameObject newBackground = Instantiate(background, player.transform.position + backgroundOffset, Quaternion.identity);
            newBackground.name += currentIndex;
        }
    }
}
