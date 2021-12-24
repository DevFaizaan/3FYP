using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interact : MonoBehaviour
{
    // Start is called before the first frame update


    public bool desk;
    public GameObject timerUI;

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
            timerUI.SetActive(true);
            dialogBox.SetActive(false);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in range");

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
