using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class UI_Clock : MonoBehaviour
{
    private Transform minuteHandTransform;
    private Transform hourHandTransform;

    private float minuteHandDegreesPerSecond;
    private float hourHandDegreesPerSecond;

    private float initialRealtimeSecondsElapsed = 60f * 9f;
    private float realtimeSecondsElapsed;

    private int numHoursElapsed;

    private bool isClockPaused = false;

    [SerializeField] private FMODUnity.StudioEventEmitter hourTickSound;
    [SerializeField] private FMODUnity.StudioEventEmitter endOfDaySound;

    private void Awake()
    {
        realtimeSecondsElapsed = initialRealtimeSecondsElapsed;

        minuteHandDegreesPerSecond = 360f / 60f;
        hourHandDegreesPerSecond = minuteHandDegreesPerSecond / 12f;

        minuteHandTransform = transform.Find("ClockMinuteHand");
        hourHandTransform   = transform.Find("ClockHourHand");
    }

    private void Start()
    {
        // Reset the FMOD ambience parameter to 0
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("param_ambience", 0f);

        // Call the onHourElapsed() function once for each in-game hour
        InvokeRepeating("onHourElapsed", 60f, 60f);
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
            ServiceLocator.Instance.Get<GameManager>().EndDay();

            CancelInvoke();
        }
        else
        {
            hourTickSound.Play();
        }
    }
}
