using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pomoTimer : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;

    public int Duration;

    private int remainingDuration;
    private int startDuration;
    public bool Pause;

    public GameObject timerUI;
    public GameObject startButton;
    public GameObject stopButton;
    public GameObject continueButton;
    public GameObject exitButton;
    public GameObject ResetButton;



    public void startTimer(int Second)
    {
        remainingDuration = Second;
        Pause = false;
        startDuration = Second;
        StartCoroutine(UpdateTimer());
    }



    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                if (remainingDuration != startDuration)
                {
                    //remainingDuration = startDuration;
                }
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                startButton.SetActive(false);
                //exitButton.SetActive(false);
                stopButton.SetActive(true);
                //continueButton.SetActive(true);
                yield return new WaitForSeconds(1f);
                Debug.Log("Timer has started!" + " Remaining time: " + remainingDuration);
            }


           

            yield return null;
            

        }
        OnEnd();
    }



    public void stopTimer()
    {
        Pause = true;
        stopButton.SetActive(false);
        continueButton.SetActive(true);
        ResetButton.SetActive(true);
        //exitButton.SetActive(true);
    }

    public void continueTimer()
    {

        Pause = false;
        stopButton.SetActive(true);
        ResetButton.SetActive(false);
        continueButton.SetActive(false);
        //exitButton.SetActive(false);

    }

    public void exitTimer()
    {
        startButton.SetActive(true);
        exitButton.SetActive(true);
        continueButton.SetActive(false);
        stopButton.SetActive(false);
        timerUI.SetActive(false);
        uiText.text = $"{startDuration / 60:00}:{startDuration % 60:00}";
        uiFill.fillAmount = Mathf.InverseLerp(0, Duration, startDuration);
    }

    public void resetTimer()
    {
        Pause = true;
        startButton.SetActive(true);
        continueButton.SetActive(false);
        ResetButton.SetActive(false);
        StopAllCoroutines();
        uiText.text = $"{startDuration / 60:00}:{startDuration % 60:00}";
        uiFill.fillAmount = Mathf.InverseLerp(0, Duration, startDuration);


    }
    private void OnEnd()
    {
        AchievementManager.achievementManagerInstance.AddAchievementProgress("Ach_01", 1);
        AchievementManager.achievementManagerInstance.AddAchievementProgress("Ach_05", 1);
        coinManager.coinManagerInstance.changeCoin(100);
        progressBar level = new progressBar();
        level.addExperience(100);
        //achievementManager.achievementManagerInstance.addAchievementProgress("tut", 1);
        print("End");
    }

   
}
