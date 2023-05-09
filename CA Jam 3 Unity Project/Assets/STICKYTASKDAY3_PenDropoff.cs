using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STICKYTASKDAY3_PenDropoff : MonoBehaviour
{
    [SerializeField] public GameObject luckyPenItem;
    [SerializeField] public GameObject dummyLuckyPenItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("STICKYTASKDAY3_PenDropoff trigger entered by player. Enabling lucky pen item green arrow.");
            luckyPenItem.SetActive(true);
            dummyLuckyPenItem.SetActive(false);
        }
    }
}
