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
        //instantiates the itemChecklist prefab in prefab folder
        GameObject prefabItem = Instantiate(prefabItemChecklist);

        //sets prefabItem the child of content
        prefabItem.transform.SetParent(content);

        //gets the script in the itemChecklist object "objectChecklist" script
        objectChecklist objItem = prefabItem.GetComponent<objectChecklist>();
        int index = loadIndex;

        //index value for only when loading is false
        if (!loading)
        {
            index = objChecklist.Count;
        }
      

        //set the parameters of the objectChecklist
        objItem.SetObjectInfo(name, description, index);
        //adds the checklist object to the List variable
        objChecklist.Add(objItem);
        objectChecklist temp = objItem;

        Debug.Log(objItem + " has been created and saved");


        if (objItem.confirmButton)
        {
            objItem.confirmButton.onClick.AddListener(delegate { CheckItem(temp); });
            //objItem.GetComponent<Button>().onClick.AddListener(delegate { CheckItem(temp); });
        }

        if (objItem.deleteButton)
        {
            objItem.deleteButton.onClick.AddListener(delegate { DeleteItem(temp); });
            //objItem.GetComponent<Button>().onClick.AddListener(delegate { DeleteItem(temp); });
        }
        //dont want to save if loading is true as it will overwrite the checklist
        if (!loading)
        {
            SaveJSONData();
            SwitchMode(0);
        }

        AchievementManager.achievementManagerInstance.AddAchievementProgress("Ach_04", 1);

    }

    void CheckItem(objectChecklist item)
    {
        objChecklist.Remove(item);
        SaveJSONData();
        coinManager.coinManagerInstance.changeCoin(25);
        progressBar level = new progressBar();
        level.addExperience(25);
        Destroy(item.gameObject);
        Debug.Log(item + " has been completed");
    }

    void DeleteItem(objectChecklist item)
    {
        objChecklist.Remove(item);
        SaveJSONData();
        Destroy(item.gameObject);
        Debug.Log(item + " has been removed");
    }



    void SaveJSONData()
    {
        string contents = "";

        //loop through objChecklist List
        for(int i=0; i< objChecklist.Count; i++)
        {
            //sets the parameters of the name,description and index for each task the user creates that is in the List
            ChecklistItem temp = new ChecklistItem(objChecklist[i].objName, objChecklist[i].description, objChecklist[i].index);

            //will add each object into a Json. Generates a JSON representation of the public fields of an object.
            contents += JsonUtility.ToJson(objChecklist[i]) + "\n";
        }
        

        //writes the contents to the filepath checklist
            File.WriteAllText(filePath, contents);
        
    }

    
    void LoadJSONData()
    {
        //checking if the checklist.txt file exists
        if (File.Exists(filePath))
        {
            //string is created with all the file data
            string fileData = File.ReadAllText(filePath);

            //file data is split for each individual JSON Object
            string[] split = fileData.Split('\n');
            foreach(string content in split)
            {
                if(content.Trim() != "")
                {
                    Debug.Log(content);
                    //Creates an object from its JSON representation.
                    ChecklistItem temp = JsonUtility.FromJson<ChecklistItem>(content);
                    //creates a checklist item based on the object data
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
