using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    Dictionary<ProductType, int> myItems;

    private void Start()
    {
        ResetDict();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        Product prod = other.GetComponent<Product>();

        if (prod)
        {
            StartCoroutine(prod.Return(0.5F));
            prod.gameObject.SetActive(false);
            AddToDict(prod);
        }
    }

    void AddToDict(Product item)
    {
        if (myItems.ContainsKey(item.myType))
        {
            myItems[item.myType]++;
        }
        else
        {
            myItems.Add(item.myType, 1);
        }
    }

    public Dictionary<ProductType, int> GetDict()
    {
        return myItems;
    }

    public void ResetDict()
    {
        myItems = new Dictionary<ProductType, int>();
    } 
}
