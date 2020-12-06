using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorReset : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Item prod = collision.gameObject.GetComponent<Item>();

        if (prod)
        {
            StartCoroutine(prod.Return(0.5F));
            prod.gameObject.SetActive(false);
        }
    }
}
