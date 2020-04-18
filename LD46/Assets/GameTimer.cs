﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float Timer;
    private int Hour;
    private int Minute;
    private int IncrementTime = 5;
    private bool IsMorning;

    public float MaxTime = 5.0f;
    public bool RunClock = false;
    public TextMeshProUGUI TMP;
    public TextMeshProUGUI PauseText;

    private void Awake()
    {
        Timer = 0;
        SetPauseText();
    }

    private void LateUpdate()
    {
        if (this.RunClock)
        {
            IncrementTimer();
        }
    }

    private void IncrementTimer()
    {
        this.Timer += Time.deltaTime;
        Debug.Log(Math.Floor(this.Timer));
        Debug.Log(Math.Floor(this.Timer) >= this.MaxTime);
        if (Math.Floor(this.Timer) >= this.MaxTime)
        {
            Debug.Log("Adding Time");
            AddMinutes();
            SetTimer();
            Timer = 0;
        }
    }

    private void AddMinutes()
    {
        Minute += IncrementTime;
        if (Minute == 60)
        {
            AddHour();
            Minute = 0;
        }
    }

    private void AddHour()
    {
        Hour++;
        if(Hour >= 24)
        {
            Hour = 0;
            IsMorning = true;
        }

        if(Hour >= 12 && IsMorning)
        {
            IsMorning = false;
        }
    }

    public void ToggleRunClock()
    {
        this.RunClock = !this.RunClock;
        SetPauseText();
    }

    public void SetRunClock(bool clockOn)
    {
        this.RunClock = clockOn;
        SetPauseText();
    }

    private void SetTimer()
    {
        TMP.SetText(string.Format("{0}{1}:{2}{3}{4}", AddZero(this.Hour), this.Hour, AddZero(this.Minute), this.Minute, AddMorning()));
    }

    private string AddZero(int time)
    {
        if(time < 10)
        {
            return "0";
        }
        return "";
    }

    private string AddMorning()
    {
        if(this.Hour >= 12)
        {
            return "pm";
        }
        return "am";
    }

    public void SpeedUpTime()
    {
        this.IncrementTime += 5;
        if(IncrementTime >= 30)
        {
            IncrementTime = 30;
        }
    }

    public void SlowDownTime()
    {
        this.IncrementTime -= 5;
        if(IncrementTime <= 5)
        {
            IncrementTime = 5;
        }
    }

    private void SetPauseText()
    {
        if (this.RunClock)
        {
            PauseText.SetText("ii");
        }
        else
        {
            PauseText.SetText(">");
        }
    }
}
