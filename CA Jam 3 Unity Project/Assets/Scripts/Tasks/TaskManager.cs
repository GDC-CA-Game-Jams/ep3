using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public List<TaskSO> baseTasks = new();

    public Stack<TaskSO> priorityTasks = new();

    private void OnNewTaskAssigned(TaskSO task) 
    {
        if (baseTasks.Contains(task))
        {
            return;
        }
        
        // TODO: Update UI
        TaskUI.Instance.WriteTask(task.taskName);
        baseTasks.Add(task);
    }

    private void OnTaskProgress(TaskSO task)
    {
        // TODO: Update UI
    }

    private void OnTaskComplete(TaskSO task)
    {
        
    }

}
