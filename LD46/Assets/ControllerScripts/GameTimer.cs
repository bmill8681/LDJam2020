using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float Timer;
    private int Hour = 5;
    private int Minute = 50;
    private int Multiplier = 1;
    private bool IsMorning;

    public float MaxTime = 1.0f;
    public bool RunClock = false;
    public bool RunUpdate = false;
    public bool HasUpdated = false;

    public TextMeshProUGUI TMP;
    public TextMeshProUGUI PauseText;

    private void Awake()
    {
        Timer = 0;
        SetPauseText();
        SetTimer();
    }

    private void LateUpdate()
    {
        if (this.RunClock)
        {
            IncrementTimer();
        }
    }

    public void ResetGame()
    {
        this.Hour = 5;
        this.Minute = 1;
        this.Multiplier = 1;
        this.RunClock = false;
    }

    public bool GetUpdateStatus()
    {
        return RunUpdate && !HasUpdated;
    }

    public void SetRunUpdateFalse()
    {
        HasUpdated = true;
    }

    private void IncrementTimer()
    {
        if (!RunClock) return;

        this.Timer += Time.deltaTime * this.Multiplier;
        if (this.Timer >= this.MaxTime)
        {
            AddMinutes();
            SetTimer();
            Timer = 0;
        }
    }

    private void AddMinutes()
    {
        Minute++;
        if (Minute >= 60)
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

        if(Hour % 1 == 0)
        {
            RunUpdate = true;
            HasUpdated = false;
        }
    }

    public void ToggleRunClock()
    {
        this.RunClock = !this.RunClock;
        GameManagerScript.Instance.ToggleToolsDisabled();
        SetPauseText();
    }

    public void SetRunClock(bool clockOn)
    {
        this.RunClock = clockOn;
        GameManagerScript.Instance.SetToolsDisabled(!clockOn);
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
        this.Multiplier ++;
        if(this.Multiplier >= 10)
        {
            this.Multiplier = 10;
        }
    }

    public void SlowDownTime()
    {
        this.Multiplier--;
        if (this.Multiplier <= 1)
        {
            this.Multiplier = 1;
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
