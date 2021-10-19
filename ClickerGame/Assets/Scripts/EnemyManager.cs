using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public EnemyController curEnemy;

    public static EnemyManager instance;

    public Transform canvas;

    public int enemyLevel;
    public int maxEnemyLevel;
    public bool maxLevelHit;

    public int enemieLevelIncreasePrice;
    public int increaseAmount;
    public TextMeshProUGUI costText;
    public Button buyButton;

    public bool isDamageDoubled;

    public void Start()
    {
        CreateNewEnemy();
        isDamageDoubled = false;

        costText.text = enemieLevelIncreasePrice.ToString() + " Gold";
        if (enemyLevel == maxEnemyLevel)
        {
            costText.text = "Max Level";
            maxLevelHit = true;
        }
    }

    public void Update()
    {
        if (GameManager.instance.gold < enemieLevelIncreasePrice || maxLevelHit )
        {
            buyButton.interactable = false;
            Color colour = costText.color;
            colour.a = 0.5f;
            costText.color = colour;
        }
        else
        {
            buyButton.interactable = true;
            Color colour = costText.color;
            colour.a = 1f;
            costText.color = colour;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void CreateNewEnemy()
    {
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject obj = Instantiate(enemyToSpawn, canvas);

        curEnemy = obj.GetComponent<EnemyController>();
    }


    public void DefeatEnemy(GameObject enemy)
    {
        Destroy(enemy);
        CreateNewEnemy();
        GameManager.instance.BackgroundCheck();
    }

    public void LevelUpEnemies()
    {
        if (enemyLevel < maxEnemyLevel)
            enemyLevel++;
    }

    public void OnBuyLevelUp()
    {
        if (GameManager.instance.gold >= enemieLevelIncreasePrice)
        {
            LevelUpEnemies();
            GameManager.instance.TakeGold(enemieLevelIncreasePrice);
            enemieLevelIncreasePrice += increaseAmount;
            costText.text = enemieLevelIncreasePrice.ToString() + " Gold";
            if (enemyLevel == maxEnemyLevel)
            {
                costText.text = "Max Level";
                maxLevelHit = true;
            }         
            Destroy(curEnemy.gameObject);
            CreateNewEnemy();
        }
    }

    public void OnShowAd()
    {
        Debug.Log("Ad");
        AdManager.instance.PlayAdForDamage();
    }

    public void ResetEnemies()
    {
        enemieLevelIncreasePrice -= (enemyLevel * increaseAmount);
        costText.text = enemieLevelIncreasePrice.ToString() + " Gold";
        enemyLevel = 0;
        Destroy(curEnemy.gameObject);
        CreateNewEnemy();
    }
}
