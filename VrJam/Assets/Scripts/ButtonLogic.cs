using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public GameObject myButton;
    public bool pushed = false;

    float yPos = -1;

    private void OnTriggerEnter(Collider other)
    {
        if(!pushed && other.gameObject == myButton)
        {
            pushed = true;
            ButtonPressed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pushed && other.gameObject == myButton)
        {
            pushed = false;
            ButtonReleased();
        }
    }

    protected virtual void ButtonPressed()
    {
        Debug.Log("button pressed!");
    }

    protected virtual void ButtonReleased()
    {
        Debug.Log("button released!");
    }

    private void Start()
    {
        yPos = myButton.transform.localPosition.y;
    }

    private void Update()
    {
        if(myButton.transform.localPosition.y < 0)
        {
            //reset
            myButton.transform.localPosition = new Vector3(0, yPos, 0);
        }
    }
}
