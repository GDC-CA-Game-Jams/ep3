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

    [Tooltip("Name of the dialogue text file within StreamingAssets/Dialog folder, including .txt extension")]
    [SerializeField] private string dialogueFileName;

    private string[] lines = {};
    private int lineIndex = 0;

    private Coroutine routine;

    [Tooltip("How long the one given set of dialog will be there")]
    [SerializeField] private float dialogTime;

    [Tooltip("Text object to update with the dialog")]
    [SerializeField] private TMP_Text text;

    [Tooltip("Speech bubble panel to resize with text")]
    [SerializeField] private GameObject bubble;

    // Start is called before the first frame update
    void Start()
    {
        DIALOG_PATH = Application.streamingAssetsPath + "/Dialog/" + dialogueFileName;
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
            string line = lines[lineIndex];
            text.text = line;

            // Resize the speech bubble to fit the text
            text.ForceMeshUpdate(true, true);
            RectTransform bubbleRT = bubble.GetComponent<RectTransform>();
            bubbleRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, text.renderedHeight + 10);

            // Next line of dialog. At the end, just repeat the last line.
            if (lineIndex < lines.Length-1)
            {
                lineIndex++;
            }

            yield return wait;
        }
    }
}
