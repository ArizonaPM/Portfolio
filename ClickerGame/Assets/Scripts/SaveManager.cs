using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update

    public void Start()
    {
        InvokeRepeating("SaveGame", 10.0f, 10.0f);

        LoadGame();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        //saves enemies data
        PlayerPrefs.SetInt("EnemyLevel", EnemyManager.instance.enemyLevel);
        PlayerPrefs.SetInt("EnemyLevelPrice", EnemyManager.instance.enemieLevelIncreasePrice);

        //saves Ad Watch time left
        PlayerPrefs.SetFloat("AdWatchRemaining", AdManager.instance.timer);

        //saves click manager
        PlayerPrefs.SetInt("AmountOfClickers", ClickManager.instance.amountOfClickers);
        PlayerPrefs.SetInt("ClickerPrice", ClickManager.instance.autoClickerPrice);

        //saves Ascension Manager
        PlayerPrefs.SetInt("AmountOfAscensions", AscensionManager.instance.amountOfAscensions);
        PlayerPrefs.SetInt("TotalGoldGathered", AscensionManager.instance.totalGoldGathered);

        //saves Game Manager
        PlayerPrefs.SetInt("Gold", GameManager.instance.gold);

        //saves last save time
        int hour = System.DateTime.Now.Hour * 3600;
        int minute = System.DateTime.Now.Minute * 60;
        int second = System.DateTime.Now.Second;
        PlayerPrefs.SetInt("Time", (hour+minute+second));

        Debug.Log("Game Saved, time: " + (hour+minute+second).ToString());

    }

    public void LoadGame()
    {
        //loads enemies data
        EnemyManager.instance.enemyLevel = PlayerPrefs.GetInt("EnemyLevel", 0);
        EnemyManager.instance.enemieLevelIncreasePrice = PlayerPrefs.GetInt("EnemyLevelPrice", 20);

        //Loads Ad Watch time left
        AdManager.instance.timer = PlayerPrefs.GetFloat("AdWatchRemaining", 7200.0f);

        //Loads click manager
        ClickManager.instance.amountOfClickers = PlayerPrefs.GetInt("AmountOfClickers", 0);
        ClickManager.instance.autoClickerPrice = PlayerPrefs.GetInt("ClickerPrice", 10);
        if (ClickManager.instance.amountOfClickers > 0)
        {
            ClickManager.instance.autoClickerLastTime.Add(Time.time);
            ClickManager.instance.damagePerClicker.Add(ClickManager.instance.amountOfClickers);
        }

        //Load Ascension Manager
        AscensionManager.instance.amountOfAscensions = PlayerPrefs.GetInt("AmountOfAscensions", 0);
        AscensionManager.instance.totalGoldGathered = PlayerPrefs.GetInt("TotalGoldGathered", 0);

        //Load Game Manager
        GameManager.instance.gold = PlayerPrefs.GetInt("Gold", 0);

        //load last save time
        GameManager.instance.lastLoadTime = PlayerPrefs.GetInt("Time", 0);

        Debug.Log("Game Loaded");

    }

    [Button("Reset Player Data")]
    private void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();
    }

}
