using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Time Variables")]
    public int dayCounter = 1;
    public int clockTime = 9;
    public TextMeshPro timerTxt;

    [Header("GameObjects")]
    public BoxCollector box;
    public ItemPicker orderScript;
    public TextMeshPro tempText;
    public OVRScreenFade screenFader;
    public TextMeshPro specialTxt1;
    public TextMeshPro specialTxt2;

    [Header("Game Variables")]
    public bool inGame;
    public int strikes = 0;
    public int boxQuota;
    public int boxCount;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        inGame = true;

        dayCounter = 1;
        boxQuota = 3;
        clockTime = 9;
        timerTxt.text = clockTime + ":00";
        StartCoroutine(UpdateTime());
    }

    IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(60);

        clockTime++;
        if (clockTime > 12) clockTime = 1;
        timerTxt.text = clockTime + ":00";

        if (clockTime != 5) StartCoroutine(UpdateTime());
        else
        {
            box.boxEnabled = false;
            foreach (ButtonLogic b in FindObjectsOfType<ButtonLogic>()) b.buttonEnabled = false;

            screenFader.FadeOut();
            if (boxCount >= boxQuota)
            {
                //reset and load next 'day'
                StartCoroutine(StartNewDay(2.5F));
            }
            else
            {
                //go to losing screen
            }
        }
    }

    public void FinishOrder()
    {
        box.boxEnabled = false;
        box.transform.parent.GetComponent<Animator>().SetTrigger("move");
        
        StartCoroutine(GetOrderResults());
        StartCoroutine(StartNewOrder(3F));
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
            
            if (i.Value != 0 && i.Key != ItemType.Receipt) success = false;
            else if(i.Key == ItemType.Receipt)
            {
                if (dayCounter == 1) success = false;
                else if (i.Value != 1) success = false;
            }
        }

        if (!success)
        {
            strikes++;
            tempText.text = "Errors: " + strikes;
            if (strikes >= 3)
            {
                screenFader.FadeOut();
                //go to losing screen
            }
        }
        else boxCount++;
    }

    void ResetDay()
    {
        //maybe put some stats on a canvas for a little bit

        StartCoroutine(StartNewDay(3F));
    }

    IEnumerator StartNewDay(float time)
    {
        yield return new WaitForSeconds(time);

        strikes = 0;
        boxCount = 0;
        boxQuota += 2;
        dayCounter++;

        clockTime = 9;
        timerTxt.text = clockTime + ":00";

        StartCoroutine(ReturnToGame(2.5F));
    }

    IEnumerator ReturnToGame(float time)
    {
        yield return new WaitForSeconds(time);

        screenFader.FadeIn();
        box.boxEnabled = true;
        foreach (ButtonLogic b in FindObjectsOfType<ButtonLogic>()) b.buttonEnabled = true;
        StartCoroutine(UpdateTime());
        StartNewOrder(0);
    }

    IEnumerator GetOrderResults()
    {
        yield return new WaitForSeconds(1.5F);
        ResultsOfOrder();
    }

    IEnumerator StartNewOrder(float time)
    {
        yield return new WaitForSeconds(time);
        orderScript.BuildList();
        box.boxEnabled = true;
    }
}
