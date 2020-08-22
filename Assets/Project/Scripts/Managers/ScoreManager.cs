using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managing player score, adding points and saving to player prefs.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Holds current total score of player game session.
    /// </summary>
    private int score;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        AddScorePoints
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Add points to player total score.
    /// </summary>
    /// <param name="scorePoints">Amount of points to add.</param>
    public void AddScorePoints(int scorePoints)
    {
        score += scorePoints;
    }
    /*--------------------------------------------------------------------------------
        Reset
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Reset player total score.
    /// </summary>
    public void Reset()
    {
        score = 0;
    }
    /*--------------------------------------------------------------------------------
        SaveHighscore
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Save player current total score to player prefs.
    /// </summary>
    /// <returns></returns>
    public bool SaveHighscore()
    {
        bool isNewHighscore = false;
        int currentHighscore = PlayerPrefs.GetInt("HighScore");
        if (score > currentHighscore)
        {
            isNewHighscore = true;
            PlayerPrefs.SetInt("HighScore", score);
        }
        return isNewHighscore;
    }
    /*--------------------------------------------------------------------------------
        GetHighscore
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get current high score of all game sessions ever made.
    /// </summary>
    /// <returns></returns>
    public int GetHighscore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    /*################################################################################
        Attributes
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Score
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get current player total score.
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
    }
}
