using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isAttachedToPlanet = true;
    private Collider2D currentPlanetCollider;
    [SerializeField] private Animator scoreText;
    [SerializeField] private Animator startText;
    [SerializeField] private GameObject startPlatform;
    [SerializeField] private CameraFollow cameraFollowScript;
    [SerializeField] private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttachedToPlanet) // Only jump if not attached
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0) && scoreManager.score == 0)
        {
            startText.SetTrigger("fadeOut");
            scoreText.SetTrigger("fadeIn");
            startPlatform.SetActive(false);
        }
    }

    void Jump()
    {
        isAttachedToPlanet = false; // Player is now jumping
        transform.SetParent(null); // Detach from the planet
        rb.linearVelocity = transform.up * moveSpeed; // Move in local "up" direction

        // Disable the collider of the planet the player was on
        if (currentPlanetCollider != null)
        {
            currentPlanetCollider.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet")) // Check if the player hits a planet
        {
            AttachToPlanet(other.transform);

            audioSource.Play();
            scoreManager.IncreaseScore();
        }
    }

    void AttachToPlanet(Transform planet)
    {
        isAttachedToPlanet = true;
        rb.linearVelocity = Vector2.zero; // Stop Movement
        transform.SetParent(planet); // Makes the player a child of the planet

        // Align player to planet's surface
        Vector2 directionToCenter = (transform.position - planet.position).normalized; // Get direction from planet center
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Enable the collider of the new planet
        currentPlanetCollider = planet.GetComponent<Collider2D>();
        if (currentPlanetCollider != null)
        {
            currentPlanetCollider.enabled = true;
        }

        // Update the camera's target to follow the new planet
        if (cameraFollowScript != null)
        {
            cameraFollowScript.SetTarget(planet);
        }
    }
}
