using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houseManager : MonoBehaviour
{

    public GameObject[] housePrefabs;
    int houseIndex;
    public Transform position;
    public static Vector2 lastPos = new Vector2(0,0);

    public void Awake()
    {
        houseIndex = PlayerPrefs.GetInt("houseSelected", 0);
        Instantiate(housePrefabs[houseIndex], lastPos, Quaternion.identity);

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
