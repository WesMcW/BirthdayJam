using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ProductPool myPool;
    public ItemType myType;

    public IEnumerator Return(float time)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return new WaitForSeconds(time);
        //returns to location of myReset and gets added back to its pool
        myPool.Return(gameObject);
    }
}

public enum ItemType
{
    Bottle,
    Block,
    Ball
}