using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 0.25f;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed); 
    }
}
