using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text finalScoreText;
    void OnEnable()
    {
        GameEvents.OnScore += OnScore;
        GameEvents.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameEvents.OnScore -= OnScore;
        GameEvents.OnGameOver -= OnGameOver;
    }

    private void OnScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void OnGameOver()
    {        
        finalScoreText.text = scoreText.text;
        scoreText.text = "0";
    }
}
