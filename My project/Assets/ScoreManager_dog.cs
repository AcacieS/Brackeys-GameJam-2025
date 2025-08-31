using UnityEngine;
using TMPro; 


public class ScoreManager_dog : MonoBehaviour
{
    public static ScoreManager_dog Instance; 

    [Header("Score Settings")]
    public int score = 0;
    public TMP_Text scoreText; 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateScoreText();
    }

    
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}