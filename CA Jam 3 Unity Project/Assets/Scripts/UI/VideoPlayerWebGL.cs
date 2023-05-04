using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerWebGL : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private string videoFileName;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = Application.streamingAssetsPath + "/Video/" + videoFileName;
        Debug.Log(videoPlayer.url);
        videoPlayer.Play();
    }
}