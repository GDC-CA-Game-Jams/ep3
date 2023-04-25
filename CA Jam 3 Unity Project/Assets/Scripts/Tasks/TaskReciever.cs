using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class TaskReciever : MonoBehaviour
{
    [SerializeField] private TaskSO task;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ServiceLocator.Instance.Get<TaskManager>().UpdateTask(task);
        }
    }
}
