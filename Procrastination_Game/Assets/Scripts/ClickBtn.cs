using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBtn : MonoBehaviour
{
    public Text clickDisplay;
    public int clicks;

    public void Click()
    {
        clicks++;
        clickDisplay.text = clicks + " CLICKS";

        
       // AchievementManager.achievementManagerInstance.AddAchievementProgress("ok", 1);
       // AchievementManager.achievementManagerInstance.AddAchievementProgress("ok2", 1);
       // AchievementManager.achievementManagerInstance.AddAchievementProgress("clickyy", 1);
        coinManager.coinManagerInstance.changeCoin(100);
    }
}
