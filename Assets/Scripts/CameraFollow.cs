using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform target;

    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, target.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
