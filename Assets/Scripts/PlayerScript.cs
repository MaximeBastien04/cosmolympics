using NUnit.Framework;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isAttachedToPlanet = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        Debug.Log(isAttachedToPlanet);
        if (Input.GetMouseButtonDown(0) && !isAttachedToPlanet)
        {
            rb.linearVelocity = Vector2.up * moveSpeed;
        }   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet")) // Check if the player hits a planet
        {
            AttachToPlanet(other.transform);
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    void AttachToPlanet(Transform planet)
    {
        isAttachedToPlanet = true;
        rb.linearVelocity = Vector2.zero; // Stop Movement
        transform.SetParent(planet); // Makes the players a child of the planet
    }
}
