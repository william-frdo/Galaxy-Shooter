using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScreen;
    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        scoreText.text = $"SCORE: {score}";

        if (score < 999999999)
        {
            score += 10;
        }
        else
        {
            score = 999999999;
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
    }
}
