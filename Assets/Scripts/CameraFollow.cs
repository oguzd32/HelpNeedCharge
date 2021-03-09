using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offSet;

    private void LateUpdate()
    {
        transform.position = new Vector3(0, target.position.y, target.position.z) + offSet;
    }
}
