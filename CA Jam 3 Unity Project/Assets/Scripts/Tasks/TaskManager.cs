using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Services;
using UnityEngine;

enum STUDIO_EVENT_EMITTERS : int
{
    ITEM_GET,
    ITEM_PUT,
    TASK_BASE_COMPLETE,
    TASK_GET,
    TASK_STICKY_COMPLETE
}

public class TaskManager : IService
{

    public List<TaskSO> baseTasks = new();

    public Stack<TaskSO> priorityTasks = new();

    private GameObject player;

    private int tasksCompleted;

    FMODUnity.StudioEventEmitter[] studioEventEmitters;

    public TaskManager()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void AssignTask(TaskSO task)
    {
        OnNewTaskAssigned(task);
    }

    public bool UpdateTask(TaskSO task)
    {
        //returns true if player could complete the task, false otherwise
        bool completed = OnTaskProgress(task);
        return completed;
    }

    public void CompleteTask(TaskSO task)
    {
        OnTaskComplete(task);
    }


    public void Init()
    {
        tasksCompleted = 0;
        player = GameObject.FindWithTag("Player");

        studioEventEmitters = player.GetComponents<FMODUnity.StudioEventEmitter>();
    }
    
    private void OnNewTaskAssigned(TaskSO task) 
    {
        if (baseTasks.Contains(task) || priorityTasks.Contains(task))
        {
            return;
        }

        task.numCompleted = 0;
        
        if (task.isSticky)
        {
            task.onTaskAssigned.Invoke(); //invoke the ontask assigned event -> taskReciever listens to this and enables green arrow
            if (!task.noStickyAudio)//if sticky audio is not disabled
            {
                // Play new sticky task assigned sound
                studioEventEmitters[(int)STUDIO_EVENT_EMITTERS.TASK_GET].Play();
            }

            TaskUI.Instance.AddSticky(task);
            priorityTasks.Push(task);

            // Update the FMOD workload parameter (up to 3 max)
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("param_workload", (Math.Min(priorityTasks.Count, 3)));
        }
        else
        {
            Debug.Log(task);
            TaskUI.Instance.WriteTask(task);
            baseTasks.Add(task);
        }
    }

    private bool OnTaskProgress(TaskSO task)
        //returns true if player could complete the task, false otherwise
    {
        Inventory inv = player.GetComponent<Inventory>();
        if (task.noItemRequired || inv.Items.ContainsKey(task.taskItem)) //test if player does not need item OR player has the required item
        {
            if (priorityTasks.Any())
            {
                if (priorityTasks.Peek().Equals(task))
                {
                    TaskUI.Instance.UpdateSticky(task);
                    task.numCompleted++;
                    inv.RemoveItem(task.taskItem);
                }
            }
            else
            {
                if (baseTasks.Contains(task))
                {
                    if (inv.Items.ContainsKey(task.taskItem))
                    {
                        // Play item dropoff sound
                        studioEventEmitters[(int)STUDIO_EVENT_EMITTERS.ITEM_PUT].Play();
                    }

                    task.numCompleted++;
                    TaskUI.Instance.UpdateTask(task);
                    inv.RemoveItem(task.taskItem);
                }
            }

            if (task.numCompleted >= task.numRequired)
            {
                CompleteTask(task);
            }
            return true; //player made progress on task
        }
        return false; //player was unable to make progress on task
    }

    private void OnTaskComplete(TaskSO task)
    {
        if (priorityTasks.Any() && priorityTasks.Peek() == task)
        {
            // Play sticky task completion sound
            studioEventEmitters[(int)STUDIO_EVENT_EMITTERS.TASK_STICKY_COMPLETE].Play();

            TaskUI.Instance.RemoveSticky();
            priorityTasks.Pop();

            // Update the FMOD workload parameter (up to 3 max)
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("param_workload", (Math.Min(priorityTasks.Count, 3)));

            return;
        }

        if (baseTasks.Contains(task))
        {
            // Play base task completion sound
            studioEventEmitters[(int)STUDIO_EVENT_EMITTERS.TASK_BASE_COMPLETE].Play();

            TaskUI.Instance.CompleteTask(task);
            tasksCompleted++;
            baseTasks.Remove(task);
        }

        if(!baseTasks.Any())
        {
            ServiceLocator.Instance.Get<GameManager>().EndDay();
        }
    }

    public int GetTasksComplete()
    {
        return tasksCompleted;
    }

    internal void ClearTasks()
    {
        baseTasks.Clear();
        priorityTasks.Clear();
        TaskUI.Instance.ClearAll();
    }
}
