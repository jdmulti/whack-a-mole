using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour, IInit, IStateChange
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings Screen")]
    public StateMachine.State stateVisible;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    private CanvasGroup canvasGroup;

    /*################################################################################
        Functions: interface
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Init
    --------------------------------------------------------------------------------*/
    public virtual void Init()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }
    /*--------------------------------------------------------------------------------
        StateChange
    --------------------------------------------------------------------------------*/
    public virtual void StateChange(StateMachine.State newState, StateMachine.State oldState)
    {
        if(newState == stateVisible)
        {
            Visible(true);
        }
        else
        {
            Visible(false);
        }
    }
    /*--------------------------------------------------------------------------------
        Visible
    --------------------------------------------------------------------------------*/
    public virtual void Visible(bool isVisible)
    {
        canvasGroup.alpha = (isVisible)? 1 : 0;
        canvasGroup.interactable = isVisible;
        canvasGroup.blocksRaycasts = isVisible;
    }
}
