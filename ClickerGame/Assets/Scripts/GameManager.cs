using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public int startTime;
    public int lastLoadTime;
    public int diff;

    public int timeAwayBonus;

    public int gold;

    public TextMeshProUGUI goldText;

    public Sprite[] backgrounds;
    private int enemiesUntilBGChange;
    public Image backgroundImg;

    public static GameManager instance;
    private void Awake()
    {
        
        instance = this;
        enemiesUntilBGChange = 5;
        
    }

    private void Start()
    {
        CheckTimeAway();
        goldText.text = "Gold: " + gold.ToString();
    }

    public void AddGold(int amount)
    {
        gold += amount + AscensionManager.instance.ascensionBonus[AscensionManager.instance.amountOfAscensions];
        goldText.text = "Gold: " + gold.ToString();
        AscensionManager.instance.AddGoldAmount(amount + AscensionManager.instance.ascensionBonus[AscensionManager.instance.amountOfAscensions]);
    }
    public void TakeGold(int amount)
    {
        gold -= amount;
        goldText.text = "Gold: " + gold.ToString();
        SaveManager.instance.SaveGame();
    }

    public void BackgroundCheck()
    {
        enemiesUntilBGChange--;
        if (enemiesUntilBGChange == 0)
        {
            enemiesUntilBGChange = 5;
            backgroundImg.sprite = backgrounds[UnityEngine.Random.Range(0, backgrounds.Length)];

        }
    }

    public void resetScene()
    {
        gold = 0;
        goldText.text = "Gold: " + gold.ToString();
        EnemyManager.instance.ResetEnemies();
        ClickManager.instance.ResetClickers();
    }

    public void CheckTimeAway()
    {
        int hour = System.DateTime.Now.Hour * 3600;
        int minute = System.DateTime.Now.Minute * 60;
        int second = System.DateTime.Now.Second;
        startTime = hour + minute + second;
        diff = startTime - lastLoadTime;
        Debug.Log("Start Time: " + startTime + " Last Load Time: " + lastLoadTime.ToString() + " Difference: " + diff.ToString());
        if (diff > timeAwayBonus)
        {
            AddGold((ClickManager.instance.amountOfClickers * diff) / 20);
            Debug.Log((ClickManager.instance.amountOfClickers * diff / 20).ToString() + " Gold added for time away!");
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveManager.instance.SaveGame();
        }
        else
        {
            SaveManager.instance.LoadGame();
            CheckTimeAway();
        }
    }

    public void OnApplicationQuit()
    {
        SaveManager.instance.SaveGame();
    }
}
