using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    private TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void IncreaseScore(int amountToIncrease) // made public so other classes can access it (Enemy)
    {
        score += amountToIncrease; // adds the amount to increase to the score
        scoreText.text = score.ToString(); // converts the score to a string where it is then displayed to the screen as text
    }
}
