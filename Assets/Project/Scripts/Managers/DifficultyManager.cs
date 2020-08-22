using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles difficulty of game during game session.
/// </summary>
public class DifficultyManager : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings")]
    /// <summary>
    /// Holds all difficulty settings scriptableobjects, which are applied during game session.
    /// </summary>
    public DifficultySettings[] difficultySettings;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Holds difficulty level during game session.
    /// </summary>
    private int difficultyLevel;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Fired whenever difficulty setting has been changed.
    /// </summary>
    public Action<DifficultySettings> OnChange;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Reset
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Reset difficulty to default settings.
    /// </summary>
    public void Reset()
    {
        difficultyLevel = difficultySettings.Length;
        OnChange?.Invoke(difficultySettings[0]);
    }
    /*--------------------------------------------------------------------------------
        CheckDifficulty
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Check difficulty to be applied during game session, based on game time and total
    /// amount of difficulty settings.
    /// </summary>
    /// <param name="maxDuration">Total duration of game session.</param>
    /// <param name="currentTime">Current game session time.</param>
    public void CheckDifficulty(float maxDuration, TimeSpan currentTime)
    {
        float multiplier = ((1.0f / difficultySettings.Length) * difficultyLevel);

        if(currentTime.Seconds < (maxDuration * multiplier))
        {
            difficultyLevel--;
            OnChange?.Invoke(difficultySettings[(difficultySettings.Length-1) - difficultyLevel]);
        }
    }
}
