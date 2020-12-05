using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFlap : MonoBehaviour
{
    public bool invert;

    float currentRotat;
    public bool closed;

    bool up;
    bool left;

    void Update()
    {
        if (!invert)
        {
            currentRotat = transform.rotation.x;
            up = transform.localEulerAngles.x < 91;
            left = Mathf.Abs(currentRotat) < 0.5F;

            if (left)
            {
                if (currentRotat < 0) transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else if (!up)
            {
                //if (currentRotat < 0) transform.rotation = new Quaternion(0, 0, 0, 1);
                if (currentRotat < 0.96F) transform.rotation = new Quaternion(0.96F, 0, 0, -0.2777777F);
            }
        }
        else
        {
            currentRotat = transform.rotation.x;
            up = transform.localEulerAngles.x > 91;
            left = Mathf.Abs(currentRotat) > 0.7F;

            if (!up && left)
            {
                if (currentRotat > -0.96F) transform.rotation = new Quaternion(-0.96F, 0, 0, -0.2777777F);
            }
            else if (!left)
            {
                if (currentRotat > 0) transform.rotation = new Quaternion(0, 0, 0, 1);
            }
        }
    }
}
