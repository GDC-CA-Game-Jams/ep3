using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    
    [Tooltip("Key to pause and unpause the game with")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    
    [Tooltip("Scene to load additivley when the game is paused.")]
    [SerializeField] private string pauseScene;

    /// <summary>
    /// True if the game is paused, false if it is not
    /// </summary>
    private bool isPaused;

    // TODO: Add an event for alerting other things when the pause state changes (if needed)
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
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
    }
}
