using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task List", menuName = "Tasks/Create Task List", order = 2)]
public class TaskList : ScriptableObject
{
      public List<TaskSO> tasks;
}

