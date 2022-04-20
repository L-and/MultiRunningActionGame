using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttacher : MonoBehaviour
{
    public Transform targetTransform;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - targetTransform.position;

    }

    void FixedUpdate()
    {
       gameObject.transform.position = targetTransform.position + offset;
    }
}
