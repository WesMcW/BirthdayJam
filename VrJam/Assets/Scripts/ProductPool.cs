using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPool : MonoBehaviour
{
    GameObject myObject;

    public void Return(GameObject obj)
    {
        if (!myObject) myObject = obj;
        myObject.SetActive(true);
        myObject.transform.position = transform.position;
    }
}
