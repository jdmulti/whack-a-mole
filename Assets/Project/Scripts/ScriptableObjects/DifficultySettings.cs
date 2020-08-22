using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty Settings", menuName = "ScriptableObjects/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings")]

    /// <summary>
    /// Settings difficulty level.
    /// </summary>
    public int level;

    /// <summary>
    /// Minimum random mole appear timing.
    /// Increase to take longer to show a mole.
    /// Increasing is making the game easier.
    /// </summary>
    public float randomAppearTimingMin;

    /// <summary>
    /// Maximum random mole appear timing.
    /// Increase to take longer to show a mole.
    /// Increasing is making the game easier.
    /// </summary>
    public float randomAppearTimingMax;

    /// <summary>
    /// Minimum random mole disappear timing.
    /// Increase to show mole longer.
    /// Increasing is making the game easier.
    /// </summary>
    public float randomDisappearTimingMin;

    /// <summary>
    /// Maximum random mole disappear timing.
    /// Increase to show mole longer.
    /// Increasing is making the game easier.
    /// </summary>
    public float randomDisappearTimingMax;
}
