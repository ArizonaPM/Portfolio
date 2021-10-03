using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
