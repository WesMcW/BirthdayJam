using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool pushed = false;
    public bool buttonEnabled = true;

    protected MeshRenderer myRender;
    Material defaultMat;
    public Material pushedMat;

    private void OnTriggerEnter(Collider other)
    {
        if (buttonEnabled)
        {
            if (!pushed && other.CompareTag("Hand"))
            {
                pushed = true;
                myRender.material = pushedMat;
                ButtonPressed();
                StartCoroutine(DelayReset());
            }
        }
    }

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

    protected virtual void Start()
    {
        myRender = GetComponent<MeshRenderer>();
        defaultMat = myRender.material;
    }
}
