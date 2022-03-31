using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
   

    public int playerLevel;
    public float xpCurrent;
    public float xpRequired;

    private float timerLerp;
    private float timeDelay;

    public Image fillImg;
    public Image maskImg;
    public Text levelText;
    public Text xpText;


    public float addtionXpMultiplier;
    public float powerXpMultiplier;
    public float divsionXpMultiplier;
    
    
    // Start is called before the first frame update
    void Start()
    {
        fillImg.fillAmount = xpCurrent / xpRequired;
        maskImg.fillAmount = xpCurrent / xpRequired;
        levelText.text = "Level " + playerLevel;

       
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("xpCurrent"))
        {
            xpCurrent = PlayerPrefs.GetFloat("xpCurrent");
        }
        else
        {
            xpCurrent = 0;
            PlayerPrefs.SetFloat("xpCurrent", 0);
        }

        if (PlayerPrefs.HasKey("xpRequired"))
        {
            xpRequired = PlayerPrefs.GetFloat("xpRequired");
        }
        else
        {
            xpRequired = 0;
            PlayerPrefs.SetFloat("xpRequired", 0);
        }

        if (PlayerPrefs.HasKey("playerLevel"))
        {
            playerLevel = PlayerPrefs.GetInt("playerLevel");
        }
        else
        {
            playerLevel = 0;
            PlayerPrefs.SetInt("playerLevel", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        updateXpBar();
        if (Input.GetKeyDown(KeyCode.Equals))
            addExperience(10);

        if(xpCurrent> xpRequired)
        {
            playerLevelUp();
        }
    }

    void updateXpBar()
    {
        float xpFraction = xpCurrent / xpRequired;
        float xpFill = fillImg.fillAmount;
        if(xpFill< xpFraction)
        {
            timeDelay += Time.deltaTime;
            maskImg.fillAmount = xpFraction;

            if(timeDelay > 3)
            {
                timerLerp += Time.deltaTime;
                float completePercentage = timerLerp / 4;
                fillImg.fillAmount = Mathf.Lerp(xpFill, maskImg.fillAmount, completePercentage);
            }
        }
        xpText.text = xpCurrent + "/" + xpRequired;
    }

    public void addExperience( float xp)
    {
        xpCurrent += xp;
        timerLerp = 0f;
        timeDelay = 0f;

        PlayerPrefs.SetFloat("xpCurrent", xpCurrent);
        PlayerPrefs.SetFloat("xpRequired", xpRequired);
        PlayerPrefs.SetInt("playerLevel", playerLevel);
    }


    public void playerLevelUp()
    {
        playerLevel++;
        levelText.text = "Level " + playerLevel;
        fillImg.fillAmount = 0f;
        maskImg.fillAmount = 0f;
        xpCurrent = Mathf.RoundToInt(xpCurrent - xpRequired);
        xpRequired = requiredXpCalc();
        AchievementManager.achievementManagerInstance.AddAchievementProgress("Ach_03", 1);
        Debug.Log("test");
        Debug.Log("Player has leveled up to Level: " + playerLevel);
        Debug.Log("New Experience required " + (xpRequired - xpCurrent));
    }
    private int requiredXpCalc()
    {
        int solveRequiredXp = 0;
        for(int cyclingLevel = 1; cyclingLevel <= playerLevel; cyclingLevel++)
        {
            solveRequiredXp += (int) ((cyclingLevel + addtionXpMultiplier * Mathf.Pow(powerXpMultiplier, cyclingLevel / divsionXpMultiplier)))/10 *10;
        }
        return solveRequiredXp / 4;
    }
}
