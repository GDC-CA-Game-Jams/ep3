using UnityEngine;
using Services;
using UnityEngine.EventSystems;

public static class BootstrapperPostLoad
{
    
    /// <summary>
    /// This function is used to initialize a variety of services and setup the initial state of the game.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        //Setup Services
        //ServiceLocator.Instance.Register(new AudioManager());
        ServiceLocator.Instance.Register(new TaskManager());
        ServiceLocator.Instance.Register(new GameManager());
        // Get
        // ServiceLocator.Instance.Get<TaskManager>();
    }
}