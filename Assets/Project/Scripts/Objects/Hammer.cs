using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Enum
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// All possible hammer animations.
    /// </summary>
    public enum AnimationType
    {
        None,
        Hit,
    }

    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("References")]
    public Animator animator;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Is hammer in use or can it be used?
    /// </summary>
    private bool isInUse;

    /*--------------------------------------------------------------------------------
        Events
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Triggered whenever a mole has succesfully been hit.
    /// </summary>
    public Action<Mole> OnHitMole;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        HitMole
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Hit a mole.
    /// </summary>
    /// <param name="mole">Mole being hit.</param>
    public void HitMole(Mole mole)
    {
        if (isInUse) return;

        isInUse = true;
        this.transform.position = mole.transform.position;
        animator.Play(AnimationType.Hit.ToString().ToLower());
        StartCoroutine(NotInUseDelayed(0.4f));
        OnHitMole?.Invoke(mole);
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        NotInUseDelayed
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Coroutine, setting hammer not in use after a certain delay.
    /// This is used for setting hamer no in use after an animation is done.
    /// </summary>
    /// <param name="delay">Delay in seconds.</param>
    /// <returns></returns>
    private IEnumerator NotInUseDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInUse = false;
    }
}
