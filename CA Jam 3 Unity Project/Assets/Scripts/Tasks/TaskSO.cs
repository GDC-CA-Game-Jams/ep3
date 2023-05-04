using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Tasks/Create Task", order = 1)]
public class TaskSO : ScriptableObject
{
    public TaskType taskId;

    public bool isSticky;

    public bool noItemRequired; //player does not need an item in their inventory to complete the task- they only need to go to task location
    //this is generally true for sticky tasks

    public bool noStickyAudio; //set to true to NOT play the sticky audio when a sticky task is assigned
    //has no effect if task is not a sticky task
    //used to turn off the audio for the initial "go to your desk" task

    public string taskName;

    public int numRequired;

    public int numCompleted;

    public ItemData taskItem;

    public string GenerateUIText()
    {
        string result = taskName;
        if (numRequired > 0)
        {
            result += "(" + numCompleted + "/" + numRequired + ")";
        }

        return result;
    }
}
