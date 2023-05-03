using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class TaskReciever : MonoBehaviour
{
    [SerializeField] private TaskSO task;
    [SerializeField] private int timesRemainingUpdateTask = 1; //the maximum number of times a task can be updated at this task reciever
    // example: let's say the task reciever is a package drop off location
    // if maxTimesUpdateTask = 1, the player can only drop off 1 package here
    // if maxTimesUpdateTask = 4, the player can drop off 4 packages here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && timesRemainingUpdateTask >= 1)
        {
            timesRemainingUpdateTask -= 1; //decrease the amount of times the player can complete the task at this location again
            ServiceLocator.Instance.Get<TaskManager>().UpdateTask(task);
        }
    }
}
