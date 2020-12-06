using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    private void OnDisable()
    {
        gameObject.layer = 0;
    }

    public void TurnOn()
    {
        gameObject.layer = 8;
    }
}
