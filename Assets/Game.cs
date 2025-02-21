using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    //Clicker
    public Text scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    //Shop
    public int shop1prize;
    public Text shop1text;

    public int shop2prize;
    public Text shop2text;

    //Amount
    public Text amount1Text;
    public int amount1;
    public float amount1Profit;

    public Text amount2Text;
    public int amount2;
    public float amount2Profit;

    //Upgrade
    public int upgradePrize;
    public Text upgradeText;

    //Achievement
    public bool achievementScore;
    public bool acheieveShop;

    public Image image1;
    public Image image2;

    //Lvl System
    public int level;
    public int exp;
    public int expToNextLevel;
    public Text levelText;

    //Highest Score
    public int bestScore;
    public Text bestScoreText;

    //Button
    public Sprite sp1, sp2, sp3, sp4;
    public Image clickerButton;

    public Text tx1, tx2 , tx3 , tx4 ;

    public int changeCost = 50;
    public int cerrentbutton = 1;


    //Hit
    public GameObject plusObject;
    public Text plusText;

    // Offline Progress
    public Text offlineTimeText;
    public Text pointsGainedText;
    public GameObject offlinePanel;
    private float pendingOfflineEarnings = 0f;


    // Start is called before the first frame update
    void Start()
    {

        //Clicker
        currentScore = 0;
        hitPower = 1;
        scoreIncreasedPerSecond = 1;
        x = 0f;

        //Save before la
        shop1prize = 25;
        shop2prize = 125;
        amount1 = 0;
        amount1Profit = 1;
        amount2 = 0;
        amount2Profit = 5;

        //Reset
        //PlayerPrefs.DeleteAll();



        //Load
        currentScore = PlayerPrefs.GetInt("currentScore", 0);
        hitPower = PlayerPrefs.GetInt("hitPower", 1);
        x = PlayerPrefs.GetInt("x", 0);

        shop1prize = PlayerPrefs.GetInt("shop1prize", 25);
        shop2prize = PlayerPrefs.GetInt("shop2prize",125);
        amount1 = PlayerPrefs.GetInt("amount1", 0);
        amount1Profit = PlayerPrefs.GetInt("amount1Profit", 0);
        amount2 = PlayerPrefs.GetInt("amount2", 0);
        amount2Profit = PlayerPrefs.GetInt("amount2Profit", 0);
        upgradePrize = PlayerPrefs.GetInt("upgradePrize", 50);

        // Offline Earnings
        OfflineEarnings();
    }

    // Update is called once per frame
    void Update()
    {

        //Clicker
        scoreText.text = (int)currentScore + "$";
        scoreIncreasedPerSecond = x * Time.deltaTime;
        currentScore = currentScore + scoreIncreasedPerSecond;

        //Shop
        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        //Amount
        amount1Text.text = "Tier 1: " + amount1 + "candies $:" + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + "candies $:" + amount2Profit + "/s";

        //Upgrade
        upgradeText.text = "Cost: " + upgradePrize + " $";

        //Save
        PlayerPrefs.SetInt("currentScore", (int)currentScore);
        PlayerPrefs.SetInt("hitPower", (int)hitPower);
        PlayerPrefs.SetInt("x", (int)x);

        PlayerPrefs.SetInt("shop1prize", (int)shop1prize);
        PlayerPrefs.SetInt("shop2prize", (int)shop2prize);
        PlayerPrefs.SetInt("amount1", (int)amount1);
        PlayerPrefs.SetInt("amount1Profit", (int)amount1Profit);
        PlayerPrefs.SetInt("amount2", (int)amount2);
        PlayerPrefs.SetInt("amount2Profit", (int)amount2Profit);
        PlayerPrefs.SetInt("upgradePrize", (int)upgradePrize);


        //Hit
        //plusText.text = " + " + hitPower;


    }

    // Offline Earnings
    void OfflineEarnings()
    {
        if (PlayerPrefs.HasKey("exitTime"))
        {
            offlinePanel.SetActive(true);
            DateTime lastTime = DateTime.Parse(PlayerPrefs.GetString("exitTime"));
            DateTime currentTime = DateTime.Now;

            TimeSpan timeAway = currentTime - lastTime;

            offlineTimeText.text = string.Format("{0} Days {1} Hours {2} Mins {3} Secs",
                timeAway.Days, timeAway.Hours, timeAway.Minutes, timeAway.Seconds);

            pendingOfflineEarnings = x * (float)timeAway.TotalSeconds * 0.05f;

            pointsGainedText.text = pendingOfflineEarnings.ToString("0") + " $";

        }
        else
        {
            offlinePanel.SetActive(false);
        }
    }

    // Save exit time when closing the game
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("exitTime", DateTime.Now.ToString());
    }

    //Hit
    public void Hit()
    {
        currentScore += hitPower;


        //EXP
        exp++;

        //plusObject.SetActive(false);
        //plusObject.transform.position = new Vector3(Random.Range(465, 645 + 1), Random.Range(205, 405 + 1), 0);
        //plusObject.SetActive(false);


       // StopAllCoroutines();
        // StartCoroutine(Fly());

    }

    //Shop
    public void Shop1()
    {
        if (currentScore >= shop1prize)
        {
            currentScore -= shop1prize;
            amount1 += 1;
            amount1Profit += 1;
            x += 1;
            shop1prize += 25;
        }
    }
    public void Shop2()
    {
        if (currentScore >= shop2prize)
        {
            currentScore -= shop2prize;
            amount2 += 1;
            amount2Profit += 5;
            x += 5;
            shop2prize += 125;
        }

    }

    //Upgrade
    public void Upgrade()
    {
        if(currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize *= 3;
        }


    }
    public void CloseOfflinePanel()
    {
        currentScore += pendingOfflineEarnings;
        offlinePanel.SetActive(false);
    }

}
