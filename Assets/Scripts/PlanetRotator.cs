using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    private float rotationSpeed = 30f; // Default rotation speed (degrees per second)

    void Update()
    {
        Debug.Log(rotationSpeed);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float speed)
    {
        rotationSpeed = speed;
    }
}
