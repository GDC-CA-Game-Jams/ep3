using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpeechBubbleBehaviour : MonoBehaviour
{
    private Transform camera;

    private string DIALOG_PATH;

    private string[] lines = {};

    private Coroutine routine;

    [Tooltip("How long the one given set of dialog will be there")]
    [SerializeField] private float dialogTime;

    [Tooltip("Text object to update with the dialog")]
    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        DIALOG_PATH = Application.streamingAssetsPath + "/Dialog/chatterboxlines.txt";
        camera = Camera.main.transform;
        StreamReader reader = new StreamReader(DIALOG_PATH);
        string temp = reader.ReadToEnd();
        lines = temp.Split("\n");
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        routine = StartCoroutine(ChooseLine());
    }

    private void OnDisable()
    {
        StopCoroutine(routine);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        transform.LookAt(camera);
        */
    }

    private IEnumerator ChooseLine()
    {
        WaitForSeconds wait = new WaitForSeconds(dialogTime);
        while (true)
        {
            int index = Random.Range(0, lines.Length);
            Debug.Log("Index: " + index);
            string line = lines[index];
            text.text = line;
            yield return wait;
        }
    }
}
