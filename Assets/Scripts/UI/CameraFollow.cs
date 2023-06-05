using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0;
	[SerializeField] float cameraDist = 0;

    private void Start()
    {
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        cameraDist = (Vector2.Distance(this.transform.position, target.position + offset)) ;
        smoothSpeed = (float) (4 + cameraDist - 7 + (Math.Abs(cameraDist - 7)) * 0.25);
        SmoothFollow();   
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = (target.position + offset);
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = smoothFollow;
    }
}