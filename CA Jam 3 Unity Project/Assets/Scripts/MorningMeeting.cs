using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Services;
using UnityEngine.SceneManagement;

public class MorningMeeting : MonoBehaviour
{
    private string DIALOG_PATH;

    [Tooltip("Name of the dialogue text file within StreamingAssets/Dialog folder, including .txt extension")]
    [SerializeField] private string dialogueFileName;

    private string[] lines = { };
    private int lineIndex;

    [Tooltip("Text object to update with the dialog")]
    [SerializeField] private TMP_Text text;

    [SerializeField] public TextAsset asset;

    // Start is called before the first frame update
    void Start()
    {
        DIALOG_PATH = Application.streamingAssetsPath + "/Dialog/" + dialogueFileName;

        string temp = asset.text;
        lines = temp.Split("\n");

        lineIndex = ServiceLocator.Instance.Get<GameManager>().Day;
        Debug.Log("Line Index == " + lineIndex);

        string line = lines[lineIndex];
        text.text = line;
        text.ForceMeshUpdate(true, true);
    }

    private void OnEnable()
    {

    }

    public void UnloadSelf()
    {
        SceneManager.UnloadSceneAsync("Morning_Meeting");
    }
}
