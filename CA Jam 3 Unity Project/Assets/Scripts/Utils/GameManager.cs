using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : IService
{
    private TaskListHolder tasks;
    
    private int day = 0;

    /// <summary>
    /// True if the game is paused, false if it is not
    /// </summary>
    private bool isPaused;
    
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

    public void TogglePause(string pauseScene)
    {

        if (isPaused)
        {
            SceneManager.UnloadSceneAsync(pauseScene);
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene(pauseScene, LoadSceneMode.Additive);
            Time.timeScale = 0;
        }

        isPaused = !isPaused;
        
    }

    public void Unpause(string pauseScene)
    {
        if (!isPaused)
        {
            return;
        }

        SceneManager.UnloadSceneAsync(pauseScene);
        Time.timeScale = 1;
        isPaused = false;
    }
}
