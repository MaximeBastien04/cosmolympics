using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] private Animator deathText;
    private bool playerDied = false;

    void Update()
    {
        if (playerDied)
        {
            RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deathText.SetTrigger("fadeIn");
            playerDied = true;
        }
    }

    void RestartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Level");
        }
    }
}
