using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class proAch : MonoBehaviour
{

    public GameObject slider;
    public GameObject claim;
    public GameObject completed;

    public void claimAch()
    {
        slider.SetActive(false);
        claim.SetActive(true);
    }


    public void proClaim()
    {
        claim.SetActive(false);
        completed.SetActive(true);
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
