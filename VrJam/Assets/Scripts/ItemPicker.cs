using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPicker : MonoBehaviour
{
    ItemType item;
    public ItemType[] allItems = new ItemType[3];
    public int[] amountOfItem = new int[3];
    public bool[] wrapped = new bool[3];

    public TextMeshPro item1, item2, item3;
    public TextMeshPro orderTxt;

    string charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    // Start is called before the first frame update
    void Start()
    {
        BuildList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildList()
    {
        allItems = new ItemType[3];
        amountOfItem = new int[3];

        int rand = Random.Range(1, 6);
        allItems[0] = PickItems(rand);
        int prevNum = rand;

        for (int i = 1; i < 3; i++)
        {
            while(prevNum == rand)
            {
                rand = Random.Range(1, 6);
            }

            allItems[i] = PickItems(rand);

            if (i == 2)
            {
                while (allItems[i] == allItems[0] || allItems[i] == allItems[1])
                {
                    rand = Random.Range(1, 6);
                    allItems[i] = PickItems(rand);
                }
            }
            prevNum = rand;
        }

        for(int i = 0; i < 3; i++)
        {
            int temp = Random.Range(1, 4);
            amountOfItem[i] = temp;
        }

        wrapped = new bool[3] { false, false, false };
        string[] icon = new string[3] { "", "", "" };

        if (GameManager.instance.dayCounter > 2)
        {
            for(int i = 0; i < 3; i++)
            {
                if (amountOfItem[i] == 1)
                {
                    int wrap = Random.Range(0, 10);
                    wrapped[i] = wrap > 2;
                }

                if (wrapped[i]) icon[i] = "*";
            }
        }

        item1.text = amountOfItem[0] + icon[0] + " " + allItems[0].ToString();
        item2.text = amountOfItem[1] + icon[1] + " " + allItems[1].ToString();
        item3.text = amountOfItem[2] + icon[2] + " " + allItems[2].ToString();

        string orderString = "";
        for(int i = 0; i < 16; i++)
        {
            char randChar = charList[Random.Range(0, charList.Length)];
            orderString += randChar;
        }
        orderTxt.text = "Order: #" + orderString;
    }

    public ItemType PickItems(int x)
    {
        switch (x)
        {
            case 1:
                item = ItemType.Bottle;
                return item;
            case 2:
                item = ItemType.Block;
                return item;
            case 3:
                item = ItemType.SoccerBall;
                return item;
            case 4:
                item = ItemType.Bell;
                return item;
            default:
                item = ItemType.Snowman;
                return item;
        }
    }
}
