using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    public GameObject[] myGifts;
    public bool used;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Item>())
        {
            if (!used)
            {
                used = true;

                int rand = Random.Range(0, 2);
                GameObject gift = myGifts[rand];
                gift.GetComponent<Item>().myType = other.GetComponent<Item>().myType;
                gift.SetActive(true);
            }

            StartCoroutine(other.GetComponent<Item>().Return(0.5F));
            other.gameObject.SetActive(false);
        }
    }
}
