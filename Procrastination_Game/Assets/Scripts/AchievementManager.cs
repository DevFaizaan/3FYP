using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager achievementManagerInstance;

    [System.Serializable]
    public class Achievement
    {
        public Sprite icon;
        public string display;
        public string description;
        public string ID;
        public int current;
        public int goal;
        public bool unlocked;

        public string coins;
        public string exp;
        //public bool hidden; //optional hidden attribute
    }

   // public GameObject achievementUnlockedNotif;
    public GameObject achievementObj;

    public GameObject slider;
    public GameObject claim;
    public GameObject completed;


    [SerializeField]
    public Achievement[] achievements;

    private void Awake()
    {
        achievementManagerInstance = this;
    }

    void Start()
    {
        LoadAchievementData();
       // achievementUnlockedNotif = GameObject.Find("AchievementUnlocked");
    }

    public void AddAchievementProgress(string ID, int value)
    {
        //gets the first element if it exists in the Achievement arrary
        Achievement achievement = achievements.FirstOrDefault(x => x.ID == ID);

        if (!achievement.unlocked)
        {
            achievement.current += value;

            if(achievement.current >= achievement.goal)
            {
                achievement.current = achievement.goal;
                achievement.unlocked = true;

                //adds coins and exp for an achivement once reached
                coinManager.coinManagerInstance.changeCoin(int.Parse(achievement.coins));
                progressBar level = new progressBar();
                level.addExperience(float.Parse(achievement.exp));

                Debug.Log("Unlocked Achievement: " + achievement.display);

              
            }
            SaveAchievementData(achievement.ID);
        }
    }

   

    public void PopulateAchievementList(Transform parent)
    {
        if(parent.childCount > 0)
        {
            foreach(Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(Achievement achievement in achievements)
        {
            GameObject achievementObject = Instantiate(achievementObj, parent);

            Text achName = achievementObject.transform.Find("AchievementName").GetComponent<Text>();
            achName.text = achievement.display;

            Text achDesc = achievementObject.transform.Find("AchievementDescription").GetComponent<Text>();
            achDesc.text = achievement.description;

            Text achProgDisp = achievementObject.transform.Find("AchievementProgress").GetComponent<Text>();
            achProgDisp.text = achievement.current + "/" + achievement.goal;

            Image achIcon = achievementObject.transform.Find("AchievementIcon").GetComponent<Image>();
            achIcon.sprite = achievement.icon;

            Slider achProgBar = achievementObject.transform.Find("AchievementProgressBar").GetComponentInChildren<Slider>();
            achProgBar.maxValue = achievement.goal;
            achProgBar.value = achievement.current;

            Text achievementCoins = achievementObject.transform.Find("RewardCoins").GetComponent<Text>();
            achievementCoins.text = achievement.coins.ToString();

            Text achievementEXP = achievementObject.transform.Find("RewardEXP").GetComponent<Text>();
            achievementEXP.text = achievement.exp.ToString();



        }
    }

    

    public void LoadAchievementData()
    {
        foreach(Achievement achievement in achievements)
        {
            achievement.current = GetAchievementPref("current", achievement.ID);
            achievement.unlocked = (GetAchievementPref("unlocked", achievement.ID) == 1) ? true : false;
        }
    }

    public void SaveAchievementData(string achID)
    {
        Achievement achievement = achievements.FirstOrDefault(x => x.ID == achID);

        SetAchievementPref("current", achievement.ID, achievement.current);
        SetAchievementPref("unlocked", achievement.ID, (achievement.unlocked == true) ? 1 : 0);
    }

    public int GetAchievementPref(string type, string achID)
    {
        return PlayerPrefs.GetInt(achID + "_" + type.ToUpper());
    }

    public void SetAchievementPref(string type, string achID, int value)
    {
        PlayerPrefs.SetInt(achID + "_" + type.ToUpper(), value);
    }
}
