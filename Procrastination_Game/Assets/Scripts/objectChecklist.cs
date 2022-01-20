using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectChecklist : MonoBehaviour
{
    public string objName;
    public string description;
    public int index;

    private Text itemText;

    
    private void Start()
    {
        itemText = GetComponentInChildren<Text>();
        itemText.text = objName;
    }

    public void SetObjectInfo(string name, string description, int index)
    {
        this.objName = name;
        this.description = description;
        this.index = index;
    }
}
