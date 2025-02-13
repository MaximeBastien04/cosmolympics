using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
}
