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

    public TextMeshProUGUI item1, item2, item3;


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
        int rand = Random.Range(1, 4);
        allItems[0] = PickItems(rand);
        int prevNum = rand;

        for (int i = 1; i < 3; i++)
        {
            while(prevNum == rand)
            {
                rand = Random.Range(1, 4);
            }
            allItems[i] = PickItems(rand);
            prevNum = rand;
        }

        for(int i = 0; i < 3; i++)
        {
            int temp = Random.Range(1, 4);
            amountOfItem[i] = temp;
        }
    }

    public ItemType PickItems(int x)
    {
        switch (x)
        {
            case 1:
                item = ItemType.bottle;
                return item;
            case 2:
                item = ItemType.block;
                return item;
            default:
                item = ItemType.ball;
                return item;
        }
    }
}
