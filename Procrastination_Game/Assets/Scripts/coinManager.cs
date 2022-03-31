using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinManager : MonoBehaviour
{

    public static coinManager coinManagerInstance;
    public GameObject coin;
    public Image coinSlider;
    public Text coinText;
    public int coinAmount;




    private void Awake()
    {

        if (PlayerPrefs.HasKey("currentCoin"))
        {
            coinAmount = PlayerPrefs.GetInt("currentCoin");
            Debug.Log("Current coin amount: " + coinAmount);
        }
        else
        {
            coinAmount = 0;
            PlayerPrefs.SetInt("currentCoin", 0);
        }

        coinManagerInstance = this;
        coinText.text = coinAmount.ToString();
        coinSlider = coinSlider.GetComponent<Image>();
    }

    private void Update()
    {
        
    }

    public void changeCoin(int value)
    {
        coinAmount += value;
        coinText.text = coinAmount.ToString();
        PlayerPrefs.SetInt("currentCoin", coinAmount);
        PlayerPrefs.Save();
        Debug.Log("New coin amount: " + coinAmount);

    }








}
