using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

using STOP_MODE = FMOD.Studio.STOP_MODE;

public class AudioManager : IService
{
    //Declare Buses
    private Bus MasterBus;

    public AudioManager()
    {
        MasterBus = RuntimeManager.GetBus("bus:/");

        Debug.Log("AudioManager Created");
    }
   
}
