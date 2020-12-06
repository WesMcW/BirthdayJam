using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    private void OnDisable()
    {
        gameObject.layer = 0;
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().SetTrigger("Reset");
    }

    public void TurnOn()
    {
        gameObject.layer = 8;
        GetComponent<Animator>().enabled = false;
    }
}
