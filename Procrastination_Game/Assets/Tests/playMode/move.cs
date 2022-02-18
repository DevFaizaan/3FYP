using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class move
{

    
    // A Test behaves as an ordinary method
    [Test]
    public void characterMove_Horizontal()
    {
        Assert.AreEqual(0, Input.GetAxisRaw("Horizontal"));
    }

    [Test]
    public void characterMove_Vertical()
    {
        Assert.AreEqual(0, Input.GetAxisRaw("Vertical"));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator moveWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
