using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        int currentScore = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + currentScore;
    }
}
