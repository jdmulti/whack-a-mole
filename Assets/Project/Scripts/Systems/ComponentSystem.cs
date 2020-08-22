using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component system
/// A system to reduce the amount of Unity methods.
/// </summary>
public class ComponentSystem : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    private IInit[] initComponents;
    private ILoop[] loopComponents;
    private IStateChange[] stateChangeComponents;

    /*--------------------------------------------------------------------------------
        Events
    --------------------------------------------------------------------------------*/
    public Action Initialized;

    /*################################################################################
        Functions: unity
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Awake
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get game components and listen to statemachine changes (only place where Unity Awake exists).
    /// </summary>
    private void Awake()
    {
        StateMachine.OnStateChange += StateMachine_OnStateChange;

        initComponents = Components.GetAllOfType<IInit>();
        loopComponents = Components.GetAllOfType<ILoop>();
        stateChangeComponents = Components.GetAllOfType<IStateChange>();
    }
    /*--------------------------------------------------------------------------------
        Start
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Initialize game (only place where Unity Start exists)
    /// </summary>
    private void Start()
    {
        Init(initComponents);
        Initialized?.Invoke();
    }
    /*--------------------------------------------------------------------------------
        Update
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Update game (only place where Unity Update exists).
    /// </summary>
    private void Update()
    {
        Loop(loopComponents);
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Init
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Initialize all components in the scene at startup
    /// </summary>
    /// <param name="array">Array of components to initialize.</param>
    private static void Init(IInit[] array)
    {
        if (array == null) return;

        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            array[i].Init();
        }
    }
    /*--------------------------------------------------------------------------------
        Loop
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Update all components in the scene per frame.
    /// </summary>
    /// <param name="array">Array of components to update.</param>
    private static void Loop(ILoop[] array)
    {
        if (array == null) return;

        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            array[i].Loop();
        }
    }
    /*--------------------------------------------------------------------------------
        StateChange
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// StateChange all components in the scene.
    /// </summary>
    /// <param name="array">Components to call.</param>
    /// <param name="newState">StateMachine new state.</param>
    /// <param name="oldState">StateMachine old state.</param>
    private static void StateChange(IStateChange[] array, StateMachine.State newState, StateMachine.State oldState)
    {
        if (array != null)
        {
            int count = array.Length;
            for (int i = 0; i < count; i++)
            {
                array[i].StateChange(newState, oldState);
            }
        }
    }

    /*################################################################################
        Functions: on event
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        StateMachine_OnStateChange
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Executed whenever StateMachine changes state.
    /// </summary>
    /// <param name="newState">StateMachine new state.</param>
    /// <param name="oldState">StateMachine old state.</param>
    private void StateMachine_OnStateChange(StateMachine.State newState, StateMachine.State oldState)
    {
        StateChange(stateChangeComponents, newState, oldState);
    }
}
