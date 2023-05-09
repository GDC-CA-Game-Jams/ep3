using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.Events;

public class DayStartListener : MonoBehaviour
{

    [Tooltip("Action to take when the correct day ticks over")]
    [SerializeField] private UnityEvent correctDayAction;

    [Tooltip("Action to take when the any day except the correct one")]
    [SerializeField] private UnityEvent defaultDayAction;
    
    [Tooltip("What day to do the action on")]
    [SerializeField] private int dayToFire;
    
    private void Start()
    {
        ServiceLocator.Instance.Get<GameManager>().onDayStart += OnDayStart;
    }

    private void OnDayStart(int day)
    {
        Debug.Log("DayStartListener calling OnDayStart. We're on day " + day + " (0-indexed)");

        if (day == dayToFire)
        {
            correctDayAction.Invoke();
            return;
        }
        
        defaultDayAction.Invoke();
    }
}
