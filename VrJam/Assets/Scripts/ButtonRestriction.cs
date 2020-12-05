using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRestriction : MonoBehaviour
{
    public float yLocal;

    void Start()
    {
        
    }

    void Update()
    {
        yLocal = transform.localPosition.y;
    }
}
