using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
