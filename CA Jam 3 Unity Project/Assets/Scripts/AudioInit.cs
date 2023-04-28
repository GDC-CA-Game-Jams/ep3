using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioInit : MonoBehaviour
{
    // List of Banks to load
    [FMODUnity.BankRef]
    public List<string> Banks = new List<string>();

    // Declare FMOD Event public-reference and instance variables here
    public EventReference TEST_EVENT;
    EventInstance testEvent;

    void Awake()
    {
        StartCoroutine(LoadFMODBanksAsync());
    }

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

        print("Done async loading of FMOD sound banks.");

        print("Playing test sound.");
        testEvent = RuntimeManager.CreateInstance(TEST_EVENT);
        testEvent.set3DAttributes(RuntimeUtils.To3DAttributes(new Vector3((float)0, (float)0, (float)0)));
        testEvent.start();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
