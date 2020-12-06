using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPool : ProductPool
{
    public override void Return(GameObject obj)
    {
        if (!myObject) myObject = obj;

        myObject.GetComponent<Rigidbody>().useGravity = false;
        myObject.GetComponent<Collider>().enabled = false;

        myObject.transform.position = transform.position;
        myObject.transform.rotation = Quaternion.identity;

        StartCoroutine(Fix());
    }

    IEnumerator Fix()
    {
        yield return new WaitForSeconds(0.2F);

        myObject.SetActive(true);

        myObject.layer = 0;
        myObject.GetComponent<Animator>().enabled = true;
        myObject.GetComponent<Animator>().SetTrigger("Reset");
        myObject.GetComponent<Collider>().enabled = true;
        myObject.GetComponent<Rigidbody>().useGravity = true;

        myObject.GetComponent<Printer>().myButton.buttonEnabled = true;
    }
}
