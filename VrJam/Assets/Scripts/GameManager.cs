using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Time Variables")]
    public int currentLevel;
    public float timePassed;
    public float dayTime = 300;

    [Header("GameObjects")]
    public BoxCollector box;

    [Header("Game Variables")]
    public bool inGame;
    public int strikes = 0;
    public int roundStrikes = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (inGame)
        {
            timePassed += Time.deltaTime;

            //update game clock

            if(timePassed >= dayTime)
            {
                inGame = false;
                //calculate results
            }
        }
    }

    void ResultsOfOrder()
    {
        Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();
        foreach(ItemType i in box.GetItems())
        {
            if (itemCounts.ContainsKey(i)) itemCounts[i]++;
            else itemCounts.Add(i, 1);
        }

        // compare dict to order and see if strike is given


    }
}
