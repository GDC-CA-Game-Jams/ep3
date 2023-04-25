using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class GameManager : IService
{
    private TaskListHolder tasks;
    
    private int day = 0;

    public GameManager()
    {
        Debug.Log("GameManager Initiating!");
        tasks = Resources.Load<TaskListHolder>("Tasks/Days");
    }

    public void Init()
    {
        InitDay();
    }
    
    public void InitDay()
    {
        ServiceLocator.Instance.Get<TaskManager>().ClearTasks();
        List<TaskSO> temp = tasks.lists[day].tasks;
        for (int i = 0; i < temp.Count; ++i)
        {
            ServiceLocator.Instance.Get<TaskManager>().AssignTask(temp[i]);
        }
    }

    public void EndDay()
    {
        ServiceLocator.Instance.Get<TaskManager>().ClearTasks();
        Debug.Log("End the day!");
        ++day;
        InitDay();
    }
}
