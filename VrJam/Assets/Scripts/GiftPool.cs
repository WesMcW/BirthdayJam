using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPool : ProductPool
{
    public GiftSpawner gs;

    public override void Return(GameObject obj)
    {
        obj.SetActive(false);

        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;

        gs.used = false;
    }
}
