using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    [SerializeField] private Animator deathText;
    [SerializeField] private Animator scoreText;
    [SerializeField] private Transform planetsParent;
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
            scoreText.SetTrigger("showScore");
            playerDied = true;
            foreach (Transform planet in planetsParent)
            {
                planet.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
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
