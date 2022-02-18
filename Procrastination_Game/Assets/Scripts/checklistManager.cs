using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class checklistManager : MonoBehaviour
{

    public Transform content;
    public GameObject addTaskPanel;
    public GameObject taskBreakdownPanel;
    public Button createButton;
    public GameObject prefabItemChecklist;

    string filePath;

    private List<objectChecklist> objChecklist = new List<objectChecklist>();

    private InputField[] inputFieldAdd;

    public class ChecklistItem
    {
        public string objName;
        public string description;
        public int index;

        public ChecklistItem(string name, string description, int index)
        {
            this.objName = name;
            this.description = description;
            this.index = index;
        }
    }

    private void Start()
    {
        filePath = Application.persistentDataPath + "/checklist.txt";
        LoadJSONData();
        inputFieldAdd = addTaskPanel.GetComponentsInChildren<InputField>();

        createButton.onClick.AddListener(delegate { CreateChecklistItem(inputFieldAdd[0].text, inputFieldAdd[1].text);  });
    }

    public void SwitchMode(int mode)
    {
        switch (mode)
        {
            //show checklist main
            case 0:
                addTaskPanel.SetActive(false);
                break;


                //adding to checklist
            case 1:
                addTaskPanel.SetActive(true);
                break;

           case 2:
                taskBreakdownPanel.SetActive(false);
                break;



        
        
        
        }
    }




    void CreateChecklistItem(string name, string description, int loadIndex =0 , bool loading = false)
    {
        //gets the prefab itemChecklist
        GameObject prefabItem = Instantiate(prefabItemChecklist);

        prefabItem.transform.SetParent(content);

        //gets the script in the itemChecklist object
        objectChecklist objItem = prefabItem.GetComponent<objectChecklist>();
        int index = loadIndex;
        if (!loading)
        {
            index = objChecklist.Count;
        }
        //sets the object infro from the checlist manager
        objItem.SetObjectInfo(name, description, index);
        objChecklist.Add(objItem);
        objectChecklist temp = objItem;
        objItem.GetComponent<Button>().onClick.AddListener(delegate { CheckItem(temp); });

        if (!loading)
        {
            SaveJSONData();
            SwitchMode(0);
        }
        
    }

    void CheckItem(objectChecklist item)
    {
        objChecklist.Remove(item);
        SaveJSONData();
        coinManager.coinManagerInstance.changeCoin(25);
        Destroy(item.gameObject);
    }

    void SaveJSONData()
    {
        string contents = "";
        for(int i=0; i< objChecklist.Count; i++)
        {
            ChecklistItem temp = new ChecklistItem(objChecklist[i].objName, objChecklist[i].description, objChecklist[i].index);
            contents += JsonUtility.ToJson(objChecklist[i]) + "\n";
        }
        
            File.WriteAllText(filePath, contents);
        
    }

    
    void LoadJSONData()
    {
        if (File.Exists(filePath))
        {
            string fileData = File.ReadAllText(filePath);

            string[] split = fileData.Split('\n');
            foreach(string content in split)
            {
                if(content.Trim() != "")
                {
                    Debug.Log(content);
                    ChecklistItem temp = JsonUtility.FromJson<ChecklistItem>(content);
                    CreateChecklistItem(temp.objName, temp.description, temp.index, true);
                }
                
            }
            
        }
        else
        {
            Debug.Log("no file path");
        }
        
    }

}
