using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPool : ProductPool
{
    public GiftSpawner gs;

    public override void Return(GameObject obj)
    {
        obj.SetActive(false);

        myObject.transform.position = transform.position;
        myObject.transform.rotation = Quaternion.identity;

        gs.used = false;
    }
}
