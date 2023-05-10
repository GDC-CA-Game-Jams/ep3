using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainGameScene : MonoBehaviour
{
    // This function is to stop the music stuttering that was ocurring when switching from main menu to game
    //1) disable play button 
    //2) stop main menu music, so it has a second to fade out properly
    //3) fire a coroutine that waits one second and then loads the level

    public Button startButton;
    public string sceneToLoad;
    public void LoadMainScene()
    {
        startButton.enabled = false; ; // Does Not Use Disabled Color And Cant Click It
        //TO DO: STOP THE MUSIC
        Debug.Log("main menu music stopped");
        StartCoroutine(Wait1SecLoadGame());      
    }


    //coroutine to wait 1 sec then load main scene
    IEnumerator Wait1SecLoadGame()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Loading main game scene");
        SceneManager.LoadScene(sceneToLoad);
    }
}
