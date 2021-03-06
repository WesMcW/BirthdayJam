﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    public bool boxEnabled;

    List<ItemType> myItems;
    List<bool> itemGifted;

    private void Start()
    {
        ResetItems();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (boxEnabled)
        {
            Item prod = other.GetComponent<Item>();

            if (prod)
            {
                StartCoroutine(prod.Return(0.5F));
                prod.gameObject.SetActive(false);
                myItems.Add(prod.myType);
                itemGifted.Add(prod.isGift);
            }
        }
    }

    public List<ItemStuff> GetItems()
    {
        List<ItemStuff> structList = new List<ItemStuff>();

        for(int i = 0; i < myItems.Count; i++)
        {
            ItemStuff newStruct = new ItemStuff();
            newStruct.type = myItems[i];
            newStruct.wrapped = itemGifted[i];
            structList.Add(newStruct);
        }

        Debug.Log(structList.Count);
        return structList;
    }

    public void ResetItems()
    {
        myItems = new List<ItemType>();
        itemGifted = new List<bool>();
        boxEnabled = true;
    } 
}

public struct ItemStuff
{
    public ItemType type;
    public bool wrapped;
}
