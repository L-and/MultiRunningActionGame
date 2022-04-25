using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라를 플레이어에게 붙여주는 스크립트
public class CameraAttacher : MonoBehaviour
{
    public Transform targetTransform;

    void FixedUpdate()
    {
       gameObject.transform.position = targetTransform.position;
    }
}
