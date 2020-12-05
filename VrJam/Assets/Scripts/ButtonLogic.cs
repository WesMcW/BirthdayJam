using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    //public GameObject myButton;
    public bool pushed = false;

    protected MeshRenderer myRender;
    Material defaultMat;
    public Material pushedMat;

    float yPos = -1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        //if(!pushed && other.gameObject == myButton)
        if(!pushed && other.CompareTag("Hand"))
        {
            pushed = true;
            myRender.material = pushedMat;
            ButtonPressed();
            StartCoroutine(DelayReset());
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        //if (pushed && other.gameObject == myButton)
        if (pushed && other.CompareTag("Hand"))
        {
            pushed = false;
            ButtonReleased();
        }
    }*/

    protected virtual void ButtonPressed()
    {
        Debug.Log("button pressed!");
    }

    IEnumerator DelayReset()
    {
        yield return new WaitForSeconds(3F);
        myRender.material = defaultMat;
        pushed = false;
        ButtonReleased();
    }

    protected virtual void ButtonReleased()
    {
        Debug.Log("button released!");
    }

    private void Start()
    {
        //yPos = myButton.transform.localPosition.y;
        myRender = GetComponent<MeshRenderer>();
        defaultMat = myRender.material;
    }

    /*private void Update()
    {
        if(myButton.transform.localPosition.y < 0)
        {
            //reset
            myButton.transform.localPosition = new Vector3(0, yPos, 0);
        }
    }*/
}
