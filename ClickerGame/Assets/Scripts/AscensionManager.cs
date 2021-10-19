using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AscensionManager : MonoBehaviour
{
    public int[] ascensionCount;
    public int[] ascensionBonus;
    public int totalGoldGathered;
    public int amountOfAscensions;
    public TextMeshProUGUI countText;
    public Image ascensionBarFilled;
    public Button ascendButton;

    public TextMeshProUGUI amountOfAscensionsText;
    public TextMeshProUGUI ascensionBonusText;



    public static AscensionManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ascensionBarFilled.fillAmount = (float)totalGoldGathered / (float)ascensionCount[amountOfAscensions];
        countText.text = "Cash Gathered: " + totalGoldGathered.ToString() + " / " + ascensionCount[amountOfAscensions].ToString();
        if (totalGoldGathered < ascensionCount[amountOfAscensions])
            ascendButton.interactable = false;

        if (amountOfAscensions == 0)
        {
            amountOfAscensionsText.text = "";
            ascensionBonusText.text = "";
        }
        else
        {
            amountOfAscensionsText.text = "Number of Ascensions: " + amountOfAscensions.ToString();
            ascensionBonusText.text = "Ascension bonus: " + ascensionBonus[amountOfAscensions] + " Gold/Kill";
        }
    }


    public void AddGoldAmount(int amount)
    {
        if (totalGoldGathered < ascensionCount[amountOfAscensions])
        {
            totalGoldGathered += amount;
        }
        if (totalGoldGathered >= ascensionCount[amountOfAscensions])
        {
            totalGoldGathered = ascensionCount[amountOfAscensions];
            ascendButton.interactable = true;
        }
        ascensionBarFilled.fillAmount = (float)totalGoldGathered / (float)ascensionCount[amountOfAscensions];
        countText.text = "Cash Gathered: " + totalGoldGathered.ToString() + " / " + ascensionCount[amountOfAscensions].ToString();
    }

    public void OnClickAscend()
    {
        amountOfAscensions++;
        totalGoldGathered = 0;
        ascendButton.interactable = false;
        GameManager.instance.resetScene();
        ascensionBarFilled.fillAmount = 0.0f;
        countText.text = "Cash Gathered: " + totalGoldGathered.ToString() + " / " + ascensionCount[amountOfAscensions].ToString();

        amountOfAscensionsText.text = "Number of Ascensions: " + amountOfAscensions.ToString();
        ascensionBonusText.text = "Ascension bonus: "+ ascensionBonus[amountOfAscensions] + " Gold/Kill";
        SaveManager.instance.SaveGame();
    }

}
