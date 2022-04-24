using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttacher : MonoBehaviour
{
    public Transform targetTransform;

    void FixedUpdate()
    {
       gameObject.transform.position = targetTransform.position;
    }
}
