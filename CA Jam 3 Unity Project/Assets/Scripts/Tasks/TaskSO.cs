using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Create Task", order = 2)]
public class TaskSO : ScriptableObject
{
    public TaskType taskId;

    public bool isSticky;

    public string taskName;

    public int numRequired;

    public int numCompleted;

    public ItemData taskItem;
}
