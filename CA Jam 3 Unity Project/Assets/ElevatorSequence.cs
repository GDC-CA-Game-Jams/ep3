using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorSequence : MonoBehaviour
{
    [SerializeField] private string elevatorSequenceScene;

    [SerializeField] private string sceneToLoad;

    [SerializeField] private float delaySec;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadScene", delaySec);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(elevatorSequenceScene);
    }
}
