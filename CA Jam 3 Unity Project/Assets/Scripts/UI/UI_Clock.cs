using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Clock : MonoBehaviour
{
    private Transform minuteHandTransform;
    private Transform hourHandTransform;

    private float minuteHandDegreesPerSecond;
    private float hourHandDegreesPerSecond;

    private float realtimeSecondsElapsed = 60f * 9f;

    private void Awake()
    {
        minuteHandDegreesPerSecond = 360f / 60f;
        hourHandDegreesPerSecond = minuteHandDegreesPerSecond / 12f;

        minuteHandTransform = transform.Find("ClockMinuteHand");
        hourHandTransform   = transform.Find("ClockHourHand");
    }

    private void Update()
    {
        realtimeSecondsElapsed += Time.deltaTime;

        minuteHandTransform.eulerAngles = new Vector3(0, 0, -realtimeSecondsElapsed * minuteHandDegreesPerSecond);
        hourHandTransform.eulerAngles = new Vector3(0, 0, -realtimeSecondsElapsed * hourHandDegreesPerSecond);
    }
}
