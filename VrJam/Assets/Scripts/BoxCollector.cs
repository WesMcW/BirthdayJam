using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    public bool boxEnabled;

    List<ItemType> myItems;

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
            }
        }
    }

    public List<ItemType> GetItems()
    {
        return myItems;
    }

    public void ResetItems()
    {
        myItems = new List<ItemType>();
        boxEnabled = true;
    } 
}
