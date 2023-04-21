using System.Collections;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager
{
    // Declare FMOD Event public-reference and instance variables here
    EventInstance testEvent;

    public readonly string AudioReferenceFile = "Interactibles";
    AudioRefs audioRefs;

    //Declare Buses
    private Bus MasterBus;

    public AudioManager()
    {
        //Load References
        audioRefs = Resources.Load<AudioRefs>(AudioReferenceFile);
    }


    /// <summary>
    /// Initialize FMOD Instances
    /// Call this after Start
    /// </summary>
    public void InitSoundInstances()
    {
        //Initialize Events
        testEvent = RuntimeManager.CreateInstance(audioRefs.TEST_EVENT);
        
        MasterBus = RuntimeManager.GetBus("bus:/");
    }
   
}
