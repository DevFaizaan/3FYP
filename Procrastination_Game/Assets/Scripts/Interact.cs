using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interact : MonoBehaviour
{
    // Start is called before the first frame update


    public bool desk;
    public GameObject UI;

    public bool noticeBoard;
    public bool homePC;

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool activateDialog;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && activateDialog && desk==true)
        {
            UI.SetActive(true);
            dialogBox.SetActive(false);
            addAchProg();
           Debug.Log(UI + " has opened");
        }

        if (Input.GetKey(KeyCode.E) && activateDialog && noticeBoard == true)
        {
            UI.SetActive(true);
            dialogBox.SetActive(false);
            addAchProg();
           Debug.Log(UI + " has opened");
        }

        if (Input.GetKey(KeyCode.E) && activateDialog && homePC == true)
        {
            UI.SetActive(true);
            dialogBox.SetActive(false);
            addAchProg();
            Debug.Log(UI + " has opened");
            Debug.Log("UiOpenSound has been played");

        }
    }

    public void addAchProg()
    {
        AchievementManager.achievementManagerInstance.AddAchievementProgress("Ach_02", 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            Debug.Log("Achievement Progress has been updated");
            Debug.Log("The player has quit the game!");

            activateDialog = true;

            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has left the range");
            activateDialog = false;
            dialogBox.SetActive(false);
        }
    }
}
