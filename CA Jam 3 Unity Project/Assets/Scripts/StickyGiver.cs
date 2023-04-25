using System.Collections;
using System.Collections.Generic;
using Services;

using UnityEngine;

public class StickyGiver : MonoBehaviour
{
    [SerializeField]
    private TaskSO sticky;

    [SerializeField]
    private bool given = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !given)
        {
            ServiceLocator.Instance.Get<TaskManager>().AssignTask(sticky);
            given = true;
        }
    }
}
