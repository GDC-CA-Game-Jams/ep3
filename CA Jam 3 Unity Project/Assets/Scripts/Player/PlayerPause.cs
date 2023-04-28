using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    
    [Tooltip("Key to pause and unpause the game with")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    
    [Tooltip("Scene to load additivley when the game is paused.")]
    [SerializeField] private string pauseScene;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            ServiceLocator.Instance.Get<GameManager>().TogglePause(pauseScene);
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.Get<GameManager>().Unpause(pauseScene);
    }
}
