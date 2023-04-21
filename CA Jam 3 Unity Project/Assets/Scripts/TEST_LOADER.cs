using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TEST_LOADER : MonoBehaviour
{

    // List of Banks to load
    [FMODUnity.BankRef]
    public List<string> Banks = new List<string>();

    [FMODUnity.EventRef]
    public string TEST_EVENT;

    EventInstance testEvent;

    // Start is called before the first frame update
    void Start()
    {
        Banks.Add("Master");
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

        print("Playing test sound.");
        testEvent = RuntimeManager.CreateInstance(TEST_EVENT);
        testEvent.set3DAttributes( RuntimeUtils.To3DAttributes( new Vector3((float)-4.024331, (float)21.25, (float)0.9602232) ) );
        testEvent.start();

    }
}
