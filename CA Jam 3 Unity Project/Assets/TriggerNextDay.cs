using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Services;
using UnityEngine.UI;

public class TriggerNextDay : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = ServiceLocator.Instance.Get<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.EndDay();
        }
    }
}
