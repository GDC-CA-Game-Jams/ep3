using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class TaskManager : IService
{

    public List<TaskSO> baseTasks = new();

    public Stack<TaskSO> priorityTasks = new();

    private GameObject player;
    
    public TaskManager()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void AssignTask(TaskSO task)
    {
        OnNewTaskAssigned(task);
    }

    public void UpdateTask(TaskSO task)
    {
        OnTaskProgress(task);
    }

    public void CompleteTask(TaskSO task)
    {
        OnTaskComplete(task);
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
            TaskUI.Instance.AddSticky(task);
            priorityTasks.Push(task);
        }
        else
        {
            Debug.Log(task);
            TaskUI.Instance.WriteTask(task);
            baseTasks.Add(task);
        }
    }

    private void OnTaskProgress(TaskSO task)
    {
        Inventory inv = player.GetComponent<Inventory>();
        if (inv.Items.ContainsKey(task.taskItem))
        {
            if (priorityTasks.Count > 0)
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
                    task.numCompleted++;
                    TaskUI.Instance.UpdateTask(task);
                    inv.RemoveItem(task.taskItem);
                }
            }
            if (task.numCompleted >= task.numRequired)
            {
                CompleteTask(task);
            }
        }
    }

    private void OnTaskComplete(TaskSO task)
    {
        if (baseTasks.Contains(task))
        {
            TaskUI.Instance.RemoveTask(task);
            baseTasks.Remove(task);
        }

        if (priorityTasks.Contains(task))
        {
            TaskUI.Instance.RemoveSticky(task);
            priorityTasks.Pop();
        }
    }

}
