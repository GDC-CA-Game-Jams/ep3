using System.Collections;
using System.Collections.Generic;
using System;
using Services;
using UnityEngine;

public class UI_Clock : MonoBehaviour
{
    private Transform minuteHandTransform;
    private Transform hourHandTransform;

    private float minuteHandDegreesPerSecond;
    private float hourHandDegreesPerSecond;

    private float realtimeSecondsPerInGameHour = 30f;

    private float initialRealtimeSecondsElapsed;
    private float realtimeSecondsElapsed;

    private int numHoursElapsed;

    private bool isClockPaused = false;

    [SerializeField] private FMODUnity.StudioEventEmitter hourTickSound;
    [SerializeField] private FMODUnity.StudioEventEmitter endOfDaySound;

    GameManager gameManager;

    private void Awake()
    {
        initialRealtimeSecondsElapsed = realtimeSecondsPerInGameHour * 9f;

        minuteHandDegreesPerSecond = 360f / realtimeSecondsPerInGameHour;
        hourHandDegreesPerSecond = minuteHandDegreesPerSecond / 12f;

        minuteHandTransform = transform.Find("ClockMinuteHand");
        hourHandTransform   = transform.Find("ClockHourHand");
    }

    private void Start()
    {
        gameManager = ServiceLocator.Instance.Get<GameManager>();
        gameManager.onDayStart += ResetClock;

        ResetClock(0);
    }

    private void Update()
    {
        if (!isClockPaused)
        {
            realtimeSecondsElapsed += Time.deltaTime;

            minuteHandTransform.eulerAngles = new Vector3(0, 0, -realtimeSecondsElapsed * minuteHandDegreesPerSecond);
            hourHandTransform.eulerAngles = new Vector3(0, 0, -realtimeSecondsElapsed * hourHandDegreesPerSecond);
        }
    }

    private void onHourElapsed()
    {
        numHoursElapsed++;

        // Update the FMOD ambience parameter
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("param_ambience", (float)numHoursElapsed/8);

        print(numHoursElapsed + "hours elapsed. FMOD ambience parameter value: " + (float)numHoursElapsed/8);


        if (numHoursElapsed == 8)
        {
            print("End of workday!");

            isClockPaused = true;

            // Play end-of-day clock chime sound
            endOfDaySound.Play();

            // Other end-of-day logic goes here
            gameManager.EndDay();

            CancelInvoke();
        }
        else
        {
            hourTickSound.Play();
        }
    }

    private void OnEnable()
    {
        //gameManager.onDayStart += ResetClock;
    }

    private void OnDisable()
    {
        //gameManager.onDayStart -= ResetClock;
    }

    // The Game Manager onDayStart action passes an int, but it is unused here.
    private void ResetClock(int dayIndex)
    {
        Debug.Log("Resetting clock.");

        CancelInvoke("onHourElapsed");

        isClockPaused = false;

        // Reset time to 9am
        realtimeSecondsElapsed = initialRealtimeSecondsElapsed;

        // Reset the FMOD ambience parameter to 0
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("param_ambience", 0f);
        numHoursElapsed = 0;

        // Call the onHourElapsed() function once for each in-game hour
        InvokeRepeating("onHourElapsed", realtimeSecondsPerInGameHour, realtimeSecondsPerInGameHour);
    }
}
