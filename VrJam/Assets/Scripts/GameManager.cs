using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Time Variables")]
    public int currentLevel;
    public float timePassed;
    public float dayTime = 300;

    [Header("GameObjects")]
    public BoxCollector box;
    public ItemPicker orderScript;
    public TMPro.TextMeshPro tempText;

    [Header("Game Variables")]
    public bool inGame;
    public int strikes = 0;
    public int roundStrikes = 0;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;
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

    public void FinishOrder()
    {
        box.boxEnabled = false;
        box.transform.parent.GetComponent<Animator>().SetTrigger("move");
        
        StartCoroutine(GetOrderResults());
        StartCoroutine(StartNewOrder());
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
        bool success = true;
        for(int i = 0; i < 3; i++)
        {
            if (itemCounts.ContainsKey(orderScript.allItems[i]))
            {
                if (itemCounts[orderScript.allItems[i]] >= orderScript.amountOfItem[i])
                {
                    itemCounts[orderScript.allItems[i]] -= orderScript.amountOfItem[i];
                }
                else
                {
                    success = false;
                    break;
                }
            }
            else
            {
                success = false;
                break;
            }
        }

        foreach(var i in itemCounts)
        {
            if (i.Value != 0) success = false;
        }

        if (!success)
        {
            roundStrikes++;
            tempText.text = "Errors: " + roundStrikes;
        }
    }

    IEnumerator GetOrderResults()
    {
        yield return new WaitForSeconds(1.5F);
        ResultsOfOrder();
    }

    IEnumerator StartNewOrder()
    {
        yield return new WaitForSeconds(3F);
        orderScript.BuildList();
        box.boxEnabled = true;
    }
}
