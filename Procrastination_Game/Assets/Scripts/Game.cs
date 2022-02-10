using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game 
{
    public int coins;
    public float[] playerPosition;


    public Game(characterMovement movement)
    {
        playerPosition = new float[3];
        playerPosition[0] = movement.transform.position.x;
        playerPosition[1] = movement.transform.position.y;
        playerPosition[2] = movement.transform.position.z;

    }

    public Game(coinManager coin)
    {
        coins = coinManager.coinManagerInstance.coinAmount;
    }
}



