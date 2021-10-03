using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    int score;
    // Start is called before the first frame update
    void Start()
    {
        string key = GetComponentInParent<TextMeshProUGUI>().text;
        Debug.Log(key);
        score = PlayerPrefs.GetInt(key, 0);
        GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

}
