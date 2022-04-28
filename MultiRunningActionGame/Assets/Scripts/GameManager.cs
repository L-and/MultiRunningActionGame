using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance = null;
    private void Awake()
    {
        if (null == instance) // 인스턴스가 존재하지않을때
        {
            instance = this; // 이 객체를 인스턴스로 지정

            DontDestroyOnLoad(this.gameObject); // 씬이 로드되었을때 자신을 파괴하지않고 유지
        }
        else
        {
            if (instance != this) // instance가 이 객체가 아니라면
                Destroy(this.gameObject); // 다른객체가 존재한다는 뜻이므로 파괴해줌
        }
    }
}
