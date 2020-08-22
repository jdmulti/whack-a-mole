using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour, IInit
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings")]
    public float randomHideTimeMin;
    public float randomHideTimeMax;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    private Mole[] moles;
    private int randomMoleIndex;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Init
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Initialize manager.
    /// </summary>
    public void Init()
    {
        // Get all moles in scene.
        moles = Components.GetAllOfType<Mole>();

        // Reset moles to default state
        ResetMolePositions();
    }
    /*--------------------------------------------------------------------------------
        Reset
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Reset all moles position.
    /// </summary>
    public void ResetMolePositions()
    {
        int count = moles.Length;
        for (int i = 0; i < count; i++)
        {
            moles[i].Reset();
        }
    }
    /*--------------------------------------------------------------------------------
        SetMolePosition
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Assign a hole to a mole.
    /// </summary>
    /// <param name="hole">Hole to set mole position and status.</param>
    public void SetMoleHole(Hole hole)
    {
        if (hole == null) return;

        int count = moles.Length;
        for (int i = 0; i < count; i++)
        {
            if(moles[i].IsVisible == false)
            {
                moles[i].SetHole(hole);
                moles[i].Appear();
                moles[i].StartHideTimer(randomHideTimeMin, randomHideTimeMax);
                break;
            }
        }
    }
    /*--------------------------------------------------------------------------------
        IsAnyMoleVisible
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Check wether any mole is visible.
    /// </summary>
    /// <returns>Returns true if any mole is visible.</returns>
    public bool IsAnyMoleVisible()
    {
        bool isAnyMoleVisible = false;
        int count = moles.Length;
        for (int i = 0; i < count; i++)
        {
            if(moles[i].IsVisible)
            {
                isAnyMoleVisible = true;
            }
        }
        return isAnyMoleVisible;
    }
    /*--------------------------------------------------------------------------------
        GetRandomMole
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get a random mole.
    /// </summary>
    /// <returns>Returns a random mole.</returns>
    public Mole GetRandomMole()
    {
        bool canRun = true;
        while (canRun)
        {
            randomMoleIndex = Random.Range(0, moles.Length);
            if (moles[randomMoleIndex].IsVisible == true)
            {
                canRun = false;
            }
        }
        return moles[randomMoleIndex];
    }
}
