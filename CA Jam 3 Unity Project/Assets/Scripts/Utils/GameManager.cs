using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : IService
{
    private const int MAX_DAYS = 2;

    private const int TOTAL_TASKS = 10;
    
    private TaskListHolder tasks;

    private int day = 0;
    public int Day
    {
        get { return day; }
        set { day = value; }
    }

    /// <summary>
    /// End of day grade for the player
    /// 0.0-0.2: F
    /// 0.3-0.5: D
    /// 0.5-0.7: C
    /// 0.7-0.8: B
    /// 0.9-1.0: A
    /// </summary>
    public float grade;
    
    /// <summary>
    /// True if the game is paused, false if it is not
    /// </summary>
    private bool isPaused;

    private bool isInGameplay = false;

    private GameObject player;

    private Vector3 startPos;

    public Action<int> onDayStart;
    
    public GameManager()
    {
        Debug.Log("GameManager Initiating!");
        day = 0;
        tasks = Resources.Load<TaskListHolder>("Tasks/Days");
    }

    public void Init()
    {
        isInGameplay = true;
        day = 0;
        player = GameObject.FindWithTag("Player");
        startPos = player.transform.position;
        Debug.Log("[GameManager] Initing GameManager!");
        InitDay();
    }
    
    public void InitDay()
    {
        if (!isInGameplay)
        {
            return;
        }
        Debug.Log("[GameManager] Initing day!");
        if (day > MAX_DAYS)
        {
            isInGameplay = false;
            day = 0;
            grade = ServiceLocator.Instance.Get<TaskManager>().GetTasksComplete() / (float)TOTAL_TASKS;

            SceneManager.LoadScene("Win Screen", LoadSceneMode.Additive);
            return;
        }
        ServiceLocator.Instance.Get<TaskManager>().ClearTasks();
        List<TaskSO> temp = tasks.lists[day].tasks;
        for (int i = 0; i < temp.Count; ++i)
        {
            ServiceLocator.Instance.Get<TaskManager>().AssignTask(temp[i]);
        }

        SceneManager.LoadScene("Morning_Meeting", LoadSceneMode.Additive);
    }

    public void EndDay()
    {
        Debug.Log("[GameManager] Ending Day!");
        ServiceLocator.Instance.Get<TaskManager>().ClearTasks();
        player.GetComponent<Inventory>().RemoveAllItems();
        player.transform.position = startPos;
        ++day;
        InitDay();
    }

    public void Reset()
    {
        ServiceLocator.Instance.Get<TaskManager>().ClearTasks();
        player.GetComponent<Inventory>().RemoveAllItems();
        day = 0;
        isInGameplay = false;
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
