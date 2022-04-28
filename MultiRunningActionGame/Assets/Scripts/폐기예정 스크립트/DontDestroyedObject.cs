using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 객체가 파괴되는걸 막아주는 스크립트
public class DontDestroyedObject : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
