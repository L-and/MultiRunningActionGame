using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpeedChangeSystem : MonoBehaviour
{
    public float changeValue;

    PlayerController pc; // 이동속도 변경을 위한 변수
    PhotonView pv; 
    public SpriteRenderer spriteRenderer; // 색상변을 위한 변수

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        pv = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pv.IsMine && gameObject.layer == 10) // 현재 클라이언트만 충돌을 적용
        {
            speedUp(collision);
            speedDown(collision);
        }
    }

    void speedUp(Collider2D collision)
    {
        if (collision.tag == "avoidCollider") // 장애물을 회피하면
        {
            onSpeedChanged(collision);

            pc.speed += changeValue; // 이동속도 증가
            print('[' + gameObject.name + ']' + "스피드업!!");

            Invoke("offSpeedChanged", 0.5f);
        }
    }

    void speedDown(Collider2D collision)
    {
        if (collision.tag == "hitCollider") // 장애물에 충돌하면
        {
            onSpeedChanged(collision);

            pc.speed -= changeValue; // 이동속도 감소
            print('[' + gameObject.name + ']' + "스피드다운...");

            Invoke("offSpeedChanged", 0.5f);
        }
    }

    void onSpeedChanged(Collider2D collision) // 이동속도 업시
    {
        if(collision.tag == "hitCollider") // 이동속도 감소 시
            spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 색상을 투명하게 변경

        gameObject.layer = 11; // 이동속도가 계속 빨라지지않게 설정 
    }


    void offSpeedChanged()
    {
        gameObject.layer = 10; // 플레이어레이어로 돌려줌 
        spriteRenderer.color = new Color(1, 1, 1); // 초기 색상으로 돌려줌
    }

}
