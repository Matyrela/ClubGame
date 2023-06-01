using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public bool isCustomOffset;
    public Vector3 offset;

    public float smoothSpeed = 0.001f;

    private void Start()
    {
        if (!isCustomOffset)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        SmoothFollow();   
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = (target.position + offset);
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }
}