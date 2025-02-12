using NUnit.Framework;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isAttachedToPlanet = true;

    [SerializeField] private CameraFollow cameraFollowScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        Debug.Log(isAttachedToPlanet);
        if (Input.GetMouseButtonDown(0) && isAttachedToPlanet) // Only jump if not attached
        {
            Jump();
        }
    }

    void Jump()
    {
        isAttachedToPlanet = false; // Player is now jumping
        transform.SetParent(null); // Detach from the planet
        rb.linearVelocity = transform.up * moveSpeed; // Move in local "up" direction
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet")) // Check if the player hits a planet
        {
            AttachToPlanet(other.transform);

            // Rotate the player by 180 degrees around the Z-axis
            transform.Rotate(0f, 0f, 180f);
        }
    }

    void AttachToPlanet(Transform planet)
    {
        isAttachedToPlanet = true;
        rb.linearVelocity = Vector2.zero; // Stop Movement
        transform.SetParent(planet); // Makes the players a child of the planet
        
        // Update the camera's target to be the player's new parent (the planet)
        if (cameraFollowScript != null)
        {
            cameraFollowScript.SetTarget(planet); // Pass the new target to the CameraFollow script
        }
    }
}
