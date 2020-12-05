using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public ProductPool myPool;
    public ProductType myType;

    public void Return()
    {
        //returns to location of myReset and gets added back to its pool
        myPool.Return(gameObject);
    }
}

public enum ProductType
{
    thing1,
    thing2,
    thing3
}