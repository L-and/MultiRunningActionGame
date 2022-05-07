using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeSystem : MonoBehaviour
{
    public int changeValue;

    PlayerController pc;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hitCollider") // 장애물에 충돌하면
        {
            pc.speed -= changeValue; // 이동속도 감소
            print('[' + gameObject.name + ']' + "스피드다운...");
        }

        else if (collision.tag == "avoidCollider") // 장애물을 회피하면
        {
            pc.speed += changeValue; // 이동속도 증가
            print('[' + gameObject.name + ']' + "스피드업!!");
        }
    }
}
