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

    [SerializeField] private bool canCompleteTaskLimitedNumTimes = true;
    //set to true if player can complete a task a limited number of times
    //this is FALSE for sticky tasks like checking email. TRUE for drop off tasks like dropping off a package.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((canCompleteTaskLimitedNumTimes && timesRemainingUpdateTask >= 1) || canCompleteTaskLimitedNumTimes == false)
            {
                //attempt to update task
                bool completed = ServiceLocator.Instance.Get<TaskManager>().UpdateTask(task);
                //if sucessful (player had the item in inventory and was able to complete the task)
                if (completed && canCompleteTaskLimitedNumTimes)
                {
                    timesRemainingUpdateTask -= 1; //decrease the amount of times the player can complete the task at this location again
                }
            }
            
        }
    }
}
