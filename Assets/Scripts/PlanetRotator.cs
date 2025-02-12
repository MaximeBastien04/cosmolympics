using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    public float rotationSpeed = 30f; // Default rotation speed (degrees per second)

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
