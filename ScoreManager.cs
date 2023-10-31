using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton pattern
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        // Setup Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Ensure only one instance of ScoreManager exists
        }

        // Optionally: Ensure this object persists across scenes
        // DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText();  // Initialize the score text display
    }

    public void IncrementScore(int value)
    {
        score += value;  // Add the passed value to the score
        UpdateScoreText();  // Update the UI display
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;  // Update the score text to reflect the new score
    }
}
