using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    public GameObject[] characterPrefabs;
    int characterIndex;
    public static Vector2 lastPos = new Vector2(-3, 0);

    public void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("characSelected", 0);
        Instantiate(characterPrefabs[characterIndex], lastPos, Quaternion.identity);

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
