using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour, IInit
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    private Hole[] holes;
    private int randomHoleIndex;

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
        // Get all holes in scene.
        holes = Components.GetAllOfType<Hole>();
    }
    /*--------------------------------------------------------------------------------
        IsAnyHoleEmpty
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Check if there is any empty hole.
    /// </summary>
    /// <returns>Returns true if any hole is empty.</returns>
    public bool IsAnyHoleEmpty()
    {
        bool isAnyHoleEmpty = false;
        int count = holes.Length;
        for (int i = 0; i < count; i++)
        {
            if(holes[i].containsMole == false)
            {
                isAnyHoleEmpty = true;
                break;
            }
        }
        return isAnyHoleEmpty;
    }
    /*--------------------------------------------------------------------------------
        GetRandomHole
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get random hole which doesn't contain a mole yet.
    /// </summary>
    /// <returns>Returns random empty hole.</returns>
    public Hole GetRandomHole()
    {
        bool canRun = true;
        while(canRun)
        {
            randomHoleIndex = Random.Range(0, holes.Length);
            if(holes[randomHoleIndex].containsMole == false)
            {
                canRun = false;
            }
        }
        return holes[randomHoleIndex];
    }
    /*--------------------------------------------------------------------------------
        ResetHoles
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Reset all holes.
    /// </summary>
    public void ResetHoles()
    {
        int count = holes.Length;
        for (int i = 0; i < count; i++)
        {
            holes[i].containsMole = false;
        }
    }
}
