using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskReciever : MonoBehaviour
{
    [SerializeField] private ItemData requiredObject;

    [SerializeField] private TaskType task;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inv = other.GetComponent<Inventory>();
            if (inv.Items.ContainsKey(requiredObject))
            {
                inv.RemoveItem(requiredObject);
            }
        }
    }
}
