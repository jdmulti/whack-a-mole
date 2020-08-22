using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScreenScore : UIScreen
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("References")]
    public UIButton buttonNext;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textNewHighscore;
    public TextMeshProUGUI textCurrentHighscore;
    public TextMeshProUGUI textHighscore;

    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings")]
    public StateMachine.State nextScreen;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Init
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Initialize screen.
    /// </summary>
    public override void Init()
    {
        base.Init();
        buttonNext.PointerDown += ButtonNext_PointerDown;
    }
    /*--------------------------------------------------------------------------------
        StateChange
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Whenever StateMaschine state change has happened.
    /// </summary>
    /// <param name="newState">StateMachine new state.</param>
    /// <param name="oldState">StateMachine previous state.</param>
    public override void StateChange(StateMachine.State newState, StateMachine.State oldState)
    {
        base.StateChange(newState, oldState);
        if(newState == stateVisible)
        {
            SetScores();
        }
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        SetScores
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Display current player or previous highscore.
    /// </summary>
    private void SetScores()
    {
        textScore.text = GameController.Instance.scoreManager.Score.ToString();
        textHighscore.text = GameController.Instance.scoreManager.GetHighscore().ToString();

        bool isNewHighscore = GameController.Instance.scoreManager.SaveHighscore();
        if (isNewHighscore)
        {
            textNewHighscore.enabled = true;
            textCurrentHighscore.enabled = false;
            textHighscore.enabled = false;
        }
        else
        {
            textNewHighscore.enabled = false;
            textCurrentHighscore.enabled = true;
            textHighscore.enabled = true;
        }
    }

    /*################################################################################
        Functions: on event
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        ButtonNext_PointerDown
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Executed whenever next button has been pressed.
    /// </summary>
    /// <param name="uiButton">Button which has been pressed.</param>
    private void ButtonNext_PointerDown(UIButton uiButton)
    {
        StateMachine.SetState(nextScreen);
    }
}
