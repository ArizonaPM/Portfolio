using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4393850";
    private string placementId = "ClickReward";

    public static AdManager instance;

    public Button adButton;
    public TextMeshProUGUI adText;

    public float timer;
    public bool timerRunning;


    void Awake()
    {
        instance = this;
    }

    public void PlayAdForDamage()
    {
        Advertisement.Show(placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Ad finished");
        if (showResult == ShowResult.Finished)
        {
            EnemyManager.instance.isDamageDoubled = true;
            adButton.interactable = false;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
        
        if (timer == 7200.0f)
        {
            adButton.interactable = true;
        }
        else
        {
            timer -= GameManager.instance.diff;
            adButton.interactable = false;
        }
        Debug.Log(timer);
    }

    public void Update()
    {
        if (!adButton.interactable)
        {
            EnemyManager.instance.isDamageDoubled = true;
            timer -= Time.deltaTime;
            int second = (int)(timer % 60);
            int minute = (int)(timer / 60) % 60;
            int hour = (int)(timer / 3600);
            adText.text = "X2 Damage for: \n" + string.Format("{0:0}:{1:00}:{2:00}", hour, minute, second);
            if (hour <= 0 && minute <= 0 && second <= 0)
            {
                resetAd();
            }
        }

    }

    public void resetAd()
    {
        Debug.Log("Reseting Ad Timer");
        EnemyManager.instance.isDamageDoubled = false;
        adButton.interactable = true;
        adText.text = "Get X2 Damage\n(Ad Watch)";
        timer = 7200f;
        timerRunning = false;
    }


}
