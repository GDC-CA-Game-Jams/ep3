using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{

    public void TogglePause(string scene)
    {
        ServiceLocator.Instance.Get<GameManager>().TogglePause(scene);
    }
    
}
