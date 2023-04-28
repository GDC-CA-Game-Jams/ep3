using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task List Holder", menuName = "Tasks/Create Task List Holder", order = 3)]
public class TaskListHolder : ScriptableObject
{
    public List<TaskList> lists;
}
