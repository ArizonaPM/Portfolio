using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public Image healthBarFilled;
    public TextMeshProUGUI healthText;
    public int[] goldToGive;
    public int health;
    public int[] maxHealth;
    public Animation anim;
    public TextMeshProUGUI levelText;

    public void Start()
    {
        health = maxHealth[EnemyManager.instance.enemyLevel];
        healthText.text = "Health: " + health.ToString() +"/"+ maxHealth[EnemyManager.instance.enemyLevel].ToString();
        levelText.text = "Level: " + (EnemyManager.instance.enemyLevel + 1).ToString();
    }

    public void Damage(int amount)
    {
        if (EnemyManager.instance.isDamageDoubled)
        {
            health -= amount;
        }
        health -= amount;
        healthBarFilled.fillAmount = (float)health / (float)maxHealth[EnemyManager.instance.enemyLevel];
        healthText.text = "Health: " + health.ToString() + "/" + maxHealth[EnemyManager.instance.enemyLevel].ToString();

        anim.Stop();
        anim.Play();
        if (health <= 0)
        {
            Defeated();
        }
    }

    public void Defeated()
    {
        //Debug.Log("Defeated!");
        GameManager.instance.AddGold(goldToGive[EnemyManager.instance.enemyLevel]);
        EnemyManager.instance.DefeatEnemy(gameObject);
    }
}
