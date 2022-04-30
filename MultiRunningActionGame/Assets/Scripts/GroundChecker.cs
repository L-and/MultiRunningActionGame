using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public PlayerController PC;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Map") // 맵에 발 콜라이더가 부딪히면
        {
            PC.isJump = false; // 점프상태가 아님으로 변경
        }
    }
}
