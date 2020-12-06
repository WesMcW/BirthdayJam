using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintButton : ButtonLogic
{
    public GameObject receipt;
    Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = receipt.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ButtonPressed()
    {
        base.ButtonPressed();
        anim.SetTrigger("Print");
    }
}
