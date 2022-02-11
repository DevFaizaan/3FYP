using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class houseSelect : MonoBehaviour
{
    public GameObject[] HouseSkins;
    public int houseSelected;
    public Character[] houseCharacters;

    public Button characUnlockButton;
    public Text coinsText;
    public Text descText;

    private void Awake()
    {
        houseSelected = PlayerPrefs.GetInt("houseSelected", 0);
        foreach (GameObject player in HouseSkins)
        {
            player.SetActive(false);
        }

        HouseSkins[houseSelected].SetActive(true);

        foreach (Character c in houseCharacters)
        {
            if (c.characterPrice == 0)
                c.isUnlocked = true;
            else
            {
                c.isUnlocked = PlayerPrefs.GetInt(c.characterName, 0) == 0 ? false : true;
            }
        }

        UpdateButtonUI();
    }

    public void changeNextCharacter()
    {
        HouseSkins[houseSelected].SetActive(false);
        houseSelected++;
        if (houseSelected == HouseSkins.Length)
        {
            houseSelected = 0;
        }

        HouseSkins[houseSelected].SetActive(true);

        if (houseCharacters[houseSelected].isUnlocked)
        {
            PlayerPrefs.SetInt("houseSelected", houseSelected);
        }

        descText.text = houseCharacters[houseSelected].characterName;



        UpdateButtonUI();
    }

    public void changePreviousCharacter()
    {
        HouseSkins[houseSelected].SetActive(false);
        houseSelected--;
        if (houseSelected == -1)
        {
            houseSelected = HouseSkins.Length - 1;
        }

        HouseSkins[houseSelected].SetActive(true);
        if (houseCharacters[houseSelected].isUnlocked)
        {
            PlayerPrefs.SetInt("houseSelected", houseSelected);
        }

        descText.text = houseCharacters[houseSelected].characterName;
        UpdateButtonUI();
    }





    public void UpdateButtonUI()
    {

        coinsText.text = "" + PlayerPrefs.GetInt("currentCoin", 0);
        if (houseCharacters[houseSelected].isUnlocked == true)
        {
            characUnlockButton.gameObject.SetActive(false);
        }
        else
        {
            characUnlockButton.GetComponentInChildren<Text>().text = houseCharacters[houseSelected].characterPrice.ToString();
            if (PlayerPrefs.GetInt("currentCoin", 0) < houseCharacters[houseSelected].characterPrice)
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
        int price = houseCharacters[houseSelected].characterPrice;
        PlayerPrefs.SetInt("currentCoin", coins - price);
        PlayerPrefs.SetInt(houseCharacters[houseSelected].characterName, 1);
        PlayerPrefs.SetInt("houseSelected", houseSelected);
        houseCharacters[houseSelected].isUnlocked = true;
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