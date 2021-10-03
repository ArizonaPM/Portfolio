using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndFlag : MonoBehaviour
{
    public string sceneName;
    public bool lastLevel;

    public TextMeshProUGUI scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int score;
            string text = scoreText.text;
            int.TryParse(text, out score);
            SaveScore(score);

            if (lastLevel)
            {
                Debug.Log("You Win!");
                SceneManager.LoadScene(0);
            }
            else
                SceneManager.LoadScene(sceneName);
        }
    }

    public void SaveScore(int score)
    {
        string name = SceneManager.GetActiveScene().name;
        Debug.Log(name + ": " + score);
        int oldScore = PlayerPrefs.GetInt(name, -1);
        if (score > oldScore)
            PlayerPrefs.SetInt(name, score);
    }

}
