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
    [SerializeField] private GameObject greenArrow;

    private TaskManager taskManager;

    FMOD.Studio.EventInstance soundEffectFMODEvent;

    void Start()
    {
        taskManager = ServiceLocator.Instance.Get<TaskManager>();

        greenArrow.SetActive(false);

        if (task.taskItem.soundEffect.Length > 0)
        {
            Debug.Log("task.taskItem.soundEffect");
            soundEffectFMODEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/" + task.taskItem.soundEffect);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (taskManager.priorityTasks.Count == 0 || (taskManager.priorityTasks.Count > 0 && task.isSticky))
            {
                if ((canCompleteTaskLimitedNumTimes && timesRemainingUpdateTask >= 1) || canCompleteTaskLimitedNumTimes == false)
                {
                    //attempt to update task
                    bool completed = taskManager.UpdateTask(task);
                    //if sucessful (player had the item in inventory and was able to complete the task)
                    if (completed)
                    {
                        if (canCompleteTaskLimitedNumTimes)
                        {
                            timesRemainingUpdateTask -= 1; //decrease the amount of times the player can complete the task at this location again
                        }

                        greenArrow.SetActive(false); //remove the green arrow

                        soundEffectFMODEvent.start();
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        task.taskItem.onPickup += EnableGreenArrow;
        if(task.isSticky)
        {
            task.onTaskAssigned += EnableGreenArrow;
        }
    }

    private void OnDisable()
    {
        task.taskItem.onPickup -= EnableGreenArrow;
        if (task.isSticky)
        {
            task.onTaskAssigned -= EnableGreenArrow;
        }
    }

    private void EnableGreenArrow()
    {
        if(timesRemainingUpdateTask > 0)
            greenArrow.SetActive(true);
    }
}
