using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitBox : ButtonLogic
{
    protected override void ButtonPressed()
    {
        Debug.Log("button pressed!");
        GameManager.instance.FinishOrder();
    }
}
