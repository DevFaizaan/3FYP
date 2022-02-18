using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class interact
{

    
    // A Test behaves as an ordinary method
    [Test]
    public void interactWith_Desk()
    {
        GameObject desk = new GameObject();
        Interact interactScript = desk.AddComponent<Interact>();

        interactScript.desk = true;

        Assert.AreEqual(interactScript.desk, true);
        // Use the Assert class to test conditions
    }

    [Test]
    public void interactWith_Notice_Board()
    {
        GameObject noticeBoard = new GameObject();
        Interact interactScript = noticeBoard.AddComponent<Interact>();

        interactScript.noticeBoard = true;
        Assert.AreEqual(interactScript.noticeBoard, true);
        // Use the Assert class to test conditions
    }

    [Test]
    public void interactWith_Home_PC()
    {
        GameObject homePC = new GameObject();
        Interact interactScript = homePC.AddComponent<Interact>();

        interactScript.homePC = true;
        Assert.AreEqual(interactScript.homePC, true);
        // Use the Assert class to test conditions
    }

    [Test]
    public void interactWith_Shop()
    {
        GameObject shopButton = GameObject.Find("Button_Shop");
        var button = shopButton.GetComponent<Button>();

        MainMenu menu = new MainMenu();
 
        button.onClick.AddListener(menu.shopScene);
        
        string sceneName = "Shop";

       // Assert.That(sceneName, Is.EqualTo(SceneManager.GetActiveScene().name));
        Assert.That(sceneName, Is.EqualTo(SceneManager.GetSceneByName(sceneName)));
        //Interact interactScript = homePC.AddComponent<Interact>();

       // interactScript.homePC = true;
       // Assert.AreEqual(interactScript.homePC, true);
        // Use the Assert class to test conditions
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator interactWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
