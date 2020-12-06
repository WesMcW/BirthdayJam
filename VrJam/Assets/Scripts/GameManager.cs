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

    [Header("Texts")]
    public TextMeshProUGUI line1;
    public TextMeshProUGUI line2;
    public TextMeshProUGUI fired;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        specialTxt1.text = "";
        specialTxt2.text = "";

        inGame = true;

        dayCounter = 1;
        boxQuota = 3;
        clockTime = 9;
        timerTxt.text = clockTime + ":00";
        StartCoroutine(UpdateTime());

        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
        fired.gameObject.SetActive(false);
    }

    IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(40);

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
                StartCoroutine(SetCanvas(dayCounter, boxCount));
                StartCoroutine(StartNewDay(2.5F));
            }
            else
            {
                StartCoroutine(ShowFired());
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
        List<ItemStuff> itemStuffs = new List<ItemStuff>();
        foreach (ItemStuff i in box.GetItems())
        {
            if (itemCounts.ContainsKey(i.type)) itemCounts[i.type]++;
            else itemCounts.Add(i.type, 1);
            itemStuffs.Add(i);
        }

        Debug.Log(itemCounts.Count);

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
                    Debug.Log("wrong item amount");
                    success = false;
                    break;
                }
            }
            else
            {
                Debug.Log("wrong item: " + orderScript.allItems[i]);
                success = false;
                break;
            }
        }

        foreach (var i in itemCounts)
        {

            if (i.Value != 0 && i.Key != ItemType.Receipt)
            {
                Debug.Log("wrong item amount");
                success = false;
                break;
            }
            else if (i.Key == ItemType.Receipt && dayCounter != 1 && i.Value != 1)
            {
                Debug.Log("receipt error");
                success = false;
                break;
            }
        }

        if(dayCounter > 2)
        {
            for(int i = 0; i < 3; i++)
            {
                if (orderScript.wrapped[i])
                {
                    bool found = false;
                    foreach(ItemStuff item in itemStuffs)
                    {
                        if(item.type == orderScript.allItems[i] && item.wrapped)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Debug.Log("wrapped incorrect");
                        success = false;
                    }
                }
            }
        }

        if (!success)
        {
            strikes++;
            if (tempText) tempText.text = "Errors: " + strikes;
            if (strikes >= 3)
            {
                screenFader.FadeOut();
                StartCoroutine(ShowFired());
                //go to losing screen
            }
        }
        else boxCount++;

        box.ResetItems();
    }

    IEnumerator StartNewDay(float time)
    {
        yield return new WaitForSeconds(time);

        strikes = 0;
        boxCount = 0;
        boxQuota += 2;
        
        dayCounter++;
        if (dayCounter == 2) specialTxt1.text = "Add a receipt to every order";
        else if (dayCounter == 3) specialTxt2.text = "#* = wrap gift";

        clockTime = 9;
        timerTxt.text = clockTime + ":00";

        StartCoroutine(ReturnToGame(2.5F));
    }

    IEnumerator ReturnToGame(float time)
    {
        yield return new WaitForSeconds(time);

        DisableCanvas();
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

    IEnumerator ShowFired()
    {
        yield return new WaitForSeconds(1);

        fired.gameObject.SetActive(true);
    }

    IEnumerator SetCanvas(int day, int count)
    {
        yield return new WaitForSeconds(1);

        line1.gameObject.SetActive(true);
        line2.gameObject.SetActive(true);

        line1.text = "End of Day " + day;
        line2.text = "Orders Completed: " + count;
    }

    void DisableCanvas()
    {
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
    }
}
