using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TimeData : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private float currentTime;

    private int currentHoure;
    private int currentMinutes;
    private float currentSeconds;

    private int currentYear;
    private int currentMounth;
    private int currentDay;

    private void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        DateTime now = DateTime.Now;

        currentYear = now.Year;
        currentMounth = now.Month;
        currentDay = now.Day;

        currentHoure = now.Hour;
        currentMinutes = now.Minute;
        currentSeconds = now.Second;

        currentTime = currentHoure*60+currentMinutes;
        UpdateDisplayTime();
    }
    private void Update()
    {
        if(currentSeconds >= 60)
        {
            currentTime += 1;
            currentSeconds -= 60;
            UpdateNewTime();
            UpdateDisplayTime();
        }
        currentSeconds += Time.deltaTime;
    }
    private void UpdateNewTime()
    {
        currentHoure = Mathf.FloorToInt(currentTime / 60);
        currentMinutes = Mathf.FloorToInt(currentTime % 60);
        currentDay += currentHoure >= 24 ? 1 : 0;
    }
    private void UpdateDisplayTime()
    {
        string timeFormat = string.Format("{0:00}:{1:00} {2:00}.{3:00}.{4:00}", currentHoure, currentMinutes,currentDay,currentMounth,currentYear);
        if(timeText != null)
        {
            timeText.text = timeFormat;
        }
    }
}
