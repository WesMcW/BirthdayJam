using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbutton : ButtonLogic
{
    public Material newMat;
    public Material oldMat;

    protected override void ButtonPressed()
    {
        base.ButtonPressed();
        myButton.GetComponent<MeshRenderer>().material = newMat;
    }

    protected override void ButtonReleased()
    {
        base.ButtonReleased();
        myButton.GetComponent<MeshRenderer>().material = newMat;
    }
}
