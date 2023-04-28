using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LoadSoundBanks : MonoBehaviour
{

    public GameObject runButton;

    // List of Banks to load
    [FMODUnity.BankRef]
    public List<string> Banks = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadFMODBanksAsync());
    }

    // Async coroutine to load FMOD banks
    IEnumerator LoadFMODBanksAsync()
    {
        // Iterate all the Studio Banks and start them loading in the background
        // including the audio sample data
        foreach (var bank in Banks)
        {
            FMODUnity.RuntimeManager.LoadBank(bank, true);
        }

        // Keep yielding the co-routine until all the bank loading is done
        // (for platforms with asynchronous bank loading)
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        // Keep yielding the co-routine until all the sample data loading is done
        while (FMODUnity.RuntimeManager.AnySampleDataLoading())
        {
            yield return null;
        }

        print("Done async loading of FMOD stuff.");
        runButton.SetActive(true);
    }
}
