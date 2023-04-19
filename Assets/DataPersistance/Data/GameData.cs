using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]


public class GameData
{
    public long lastUpdated;
    
    public Vector3 playerPosition;
    public Dictionary<string, bool> coinsCollected;

    public GameData()
    {
        playerPosition = new Vector3(-6.8f, -3.68f, 1);
        coinsCollected = new Dictionary<string, bool>();
    }

    public int GetPercentageComplete()
    {
        int totalCollected = 0;

        foreach (bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        int percentageCompleted = -1;
        if (coinsCollected.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / coinsCollected.Count);
        }

        return percentageCompleted;

    }

}


