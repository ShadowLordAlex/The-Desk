using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("======KillCounter======")]
    public int kills;
    [Header ("======Health======")]
    public int health = 3;
    public GameObject[] healthBar;
    [Header ("======Cover and Difficulties======")]
    public int hitTreshold = 50;
    public bool outOfCover;
    
    
    //ECONOMY
    [Header ("======Economy======")]
    public int money = 100;
    public GameObject moneyTextObject;
    [HideInInspector]
    public Text moneyText;
    [Header("=======Score=======")]
    public GameObject ScoreObject;
    [HideInInspector]
    public Text Score;

    private void Start()
    {
        Score = ScoreObject.GetComponent<Text>();
        moneyText = moneyTextObject.GetComponent<Text>();
    }

    public void PLayerHit(double hitchance)
    {
        Debug.Log(hitchance);
        if (hitchance >= hitTreshold)
        {
            if (outOfCover)
            {
                health--;
                Debug.Log("That hurts");
                healthBar[health - 1].gameObject.SetActive(false);
                
            }
        }
    }

    private void Update()
    {
        Score.text = "Score: " + kills;
        moneyText.text = "Money: " + money;
    }

    //KillScore
    public void KillScore()
    {
        kills++;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            outOfCover = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            outOfCover = false;

    }
    
    //WElCOME TO THE ECONOMY SECTION OF THE CODE (RIP YOU ALEX BTW)
    public int killMoney = 50;
    
    public AmmoCreator AmmoCreatorSmall;
    
    public void AddMoney(int enemySize)
    {
        // ReSharper disable once InvalidXmlDocComment
        ///List of enemies and their value
        /// 1 = Normal enemy = 50

        switch (enemySize)
        {
            case 0:
                money += 50;
                break;
        }
    }

    public void SpendMoney(int spendCase)
    {
        // ReSharper disable once InvalidXmlDocComment
        ///List of stuff that you can buy and the price
        /// 0 = 50 = a new ammo clip
        if (money <= 0) return;
        
        switch (spendCase)
        {
            case 0:
                money -= 50;
                AmmoCreatorSmall.RefundAmmo(1);
                break;
        }
    }

    public void AddMoneyInNumber(int amount)
    {
        money += amount;
    }
}
