using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    public List<float> autoClickerLastTime = new List<float>();
    public List<int> damagePerClicker = new List<int>();
    public int autoClickerPrice;
    public int increaseAmount;
    public int amountOfClickers;
    public TextMeshProUGUI amountOfClickersText;
    public TextMeshProUGUI costText;
    public Button buyButton;

    private bool hasCondensed;

    public static ClickManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        InvokeRepeating("CondenseClicks", 10.0f, 10.0f);
        InvokeRepeating("AverageDamage", 21.0f, 20.0f);

        hasCondensed = false;

        if (amountOfClickers > 0)
        {
            amountOfClickersText.text = "X " + amountOfClickers.ToString();
            costText.text = autoClickerPrice.ToString() + " Gold";
        }
    }

    private void Update()
    {
        for (int i =0; i < autoClickerLastTime.Count; i++)
        {
            if(Time.time - autoClickerLastTime[i] >= 1.0f)
            {
                autoClickerLastTime[i] = Time.time;
                EnemyManager.instance.curEnemy.Damage(damagePerClicker[i]);
            }

            //Debug.Log(Time.time);
        }

        if (GameManager.instance.gold < autoClickerPrice)
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

    public void OnBuyAutoClicker()
    {
        if(GameManager.instance.gold >= autoClickerPrice)
        {
            GameManager.instance.TakeGold(autoClickerPrice);
            autoClickerLastTime.Add(Time.time);
            damagePerClicker.Add(1);
            autoClickerPrice += increaseAmount;
            amountOfClickers++;
            amountOfClickersText.text = "X " + amountOfClickers.ToString();
            costText.text = autoClickerPrice.ToString() + " Gold";
        }
    }

    public void CondenseClicks()
    {
        for (int i = 0; i < autoClickerLastTime.Count - 1; i++)
        {
            //Debug.Log(autoClickerLastTime[i+1] - autoClickerLastTime[i]);
            if (Mathf.Abs(autoClickerLastTime[i+1] - autoClickerLastTime[i]) <= 0.3f)
            {
                damagePerClicker[i] += damagePerClicker[i + 1];
                damagePerClicker.RemoveAt(i + 1);
                autoClickerLastTime.RemoveAt(i + 1);
                hasCondensed = true;
            }
        }
    }

    public void AverageDamage()
    {
        if (hasCondensed)
        {
            int averageDamage = amountOfClickers / autoClickerLastTime.Count;
            int remainder = amountOfClickers % autoClickerLastTime.Count;

            for (int i = 0; i < autoClickerLastTime.Count; i++)
            {
                damagePerClicker[i] = averageDamage;
            }
            damagePerClicker[0] += remainder;

            hasCondensed = false;
        }
    }

    public void ResetClickers()
    {
        autoClickerLastTime = new List<float>();
        damagePerClicker = new List<int>();
        autoClickerPrice -= amountOfClickers * increaseAmount;
        amountOfClickers = 0;
        amountOfClickersText.text = " ";
        costText.text = autoClickerPrice.ToString() + " Gold";
    }


}
