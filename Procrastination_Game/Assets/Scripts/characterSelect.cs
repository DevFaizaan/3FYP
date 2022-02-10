using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterSelect : MonoBehaviour
{

    public GameObject[] characSkins;
    public int characSelected;
    public Character[] gameCharacters;

    public Button characUnlockButton;
    public Text coinsText;
    public Text descText;

    private void Awake()
    {
        characSelected = PlayerPrefs.GetInt("characSelected", 0);
        foreach( GameObject player in characSkins)
        {
            player.SetActive(false);
        }

        characSkins[characSelected].SetActive(true);

        foreach(Character c in gameCharacters)
        {
            if (c.characterPrice == 0)
                c.isUnlocked = true;
            else
            {
                c.isUnlocked = PlayerPrefs.GetInt(c.characterName, 0) == 0  ? false : true;
            }
        }

        UpdateButtonUI();
    }

    public void changeNextCharacter()
    {
        characSkins[characSelected].SetActive(false);
        characSelected++;
        if(characSelected == characSkins.Length)
        {
            characSelected = 0;
        }

        characSkins[characSelected].SetActive(true);

        if (gameCharacters[characSelected].isUnlocked)
        {
            PlayerPrefs.SetInt("characSelected", characSelected);
        }

        descText.text = gameCharacters[characSelected].characterName;



        UpdateButtonUI();
    }

    public void changePreviousCharacter()
    {
        characSkins[characSelected].SetActive(false);
        characSelected--;
        if (characSelected == -1)
        {
            characSelected = characSkins.Length - 1;
        }

        characSkins[characSelected].SetActive(true);
        if (gameCharacters[characSelected].isUnlocked)
        {
            PlayerPrefs.SetInt("characSelected", characSelected);
        }

        descText.text = gameCharacters[characSelected].characterName;
        UpdateButtonUI();
    }





    public void UpdateButtonUI()
    {
        
        coinsText.text = "" + PlayerPrefs.GetInt("currentCoin", 0);
       if ( gameCharacters[characSelected].isUnlocked == true)
        {
            characUnlockButton.gameObject.SetActive(false);
        }
        else
        {
            characUnlockButton.GetComponentInChildren<Text>().text = gameCharacters[characSelected].characterPrice.ToString();
            if (PlayerPrefs.GetInt("currentCoin", 0) < gameCharacters[characSelected].characterPrice)
            {
                characUnlockButton.gameObject.SetActive(true);
                characUnlockButton.interactable = false;
                
            }
            else
            {
                characUnlockButton.gameObject.SetActive(true);
                characUnlockButton.interactable = true;
            }
        }
    }

    public void UnlockCharacter()
    {
        int coins = PlayerPrefs.GetInt("currentCoin", 0);
        int price = gameCharacters[characSelected].characterPrice;
        PlayerPrefs.SetInt("currentCoin", coins - price);
        PlayerPrefs.SetInt(gameCharacters[characSelected].characterName, 1);
        PlayerPrefs.SetInt("characSelected", characSelected);
        gameCharacters[characSelected].isUnlocked = true;
        UpdateButtonUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
