using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPool : MonoBehaviour
{
    protected GameObject myObject;

    public virtual void Return(GameObject obj)
    {
        if (!myObject) myObject = obj;
        myObject.SetActive(true);
        myObject.transform.position = transform.position;
        myObject.transform.rotation = Quaternion.identity;
        myObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
