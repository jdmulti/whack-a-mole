using System;
using UnityEngine;

public class CountdownTimer : MonoBehaviour, ILoop
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Holds start time of countdown, time is based on 'Time.realtimeSinceStartup'.
    /// </summary>
    private float timeStart;

    /// <summary>
    ///  Holds current time of countdown, time is based on 'Time.realtimeSinceStartup'.
    /// </summary>
    private float timeNow;

    /// <summary>
    /// Total duration of countdown.
    /// </summary>
    private float maxDuration;

    /// <summary>
    /// Current elapsed time.
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// If countdown is enabled.
    /// </summary>
    private bool isEnabled;

    /// <summary>
    /// Current time of countdown.
    /// </summary>
    private TimeSpan currentTime;

    /*--------------------------------------------------------------------------------
        Events
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Fired when countdown has finished
    /// </summary>
    public Action OnFinished;

    /*################################################################################
        Function: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        SetCountdown
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Set countdown duration in seconds.
    /// </summary>
    /// <param name="durationInSeconds">Countdown total duration in seconds.</param>
    public void SetCountdown(float durationInSeconds)
    {
        maxDuration = durationInSeconds;
        currentTime = TimeSpan.FromSeconds(maxDuration);
    }
    /*--------------------------------------------------------------------------------
        CountdownStart
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Start the countdown.
    /// </summary>
    public void CountdownStart()
    {
        timeStart = Time.realtimeSinceStartup;
        isEnabled = true;
    }
    /*--------------------------------------------------------------------------------
        CountdownStop
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Stop the countdown.
    /// </summary>
    public void CountdownStop()
    {
        isEnabled = false;
    }
    /*--------------------------------------------------------------------------------
        Loop
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Update countdown (Unity Update alternative)
    /// </summary>
    public void Loop()
    {
        if (!isEnabled) return;

        if (IsCountdownFinished())
        {
            isEnabled = false;
            OnFinished?.Invoke();
        }
    }

    /*################################################################################
        Function: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        IsCountdownFinished
    --------------------------------------------------------------------------------*/
    private bool IsCountdownFinished()
    {
        timeNow = Time.realtimeSinceStartup;
        elapsedTime = (timeNow - timeStart);
        currentTime = (TimeSpan.FromSeconds(maxDuration) - TimeSpan.FromSeconds(elapsedTime));
        return (elapsedTime >= maxDuration);
    }

    /*################################################################################
        Attributes
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        CurrentTime
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get countdown current time.
    /// </summary>
    public TimeSpan CurrentTime
    {
        get
        {
            return currentTime;
        }
    }
    /*--------------------------------------------------------------------------------
        MaxDuration
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get countdown maximum duration.
    /// </summary>
    public float MaxDuration
    {
        get
        {
            return maxDuration;
        }
    }
}
