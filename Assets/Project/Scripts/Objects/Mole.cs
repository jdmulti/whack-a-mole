using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Enum
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// All possible animations for mole.
    /// </summary>
    public enum AnimationType
    {
        None,
        Appear,
        Disappear,
        Hit,
        Invisible,
    }

    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("References")]
    /// <summary>
    /// Animator to play mole animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Collider to raycast against.
    /// </summary>
    public CapsuleCollider capsuleCollider;

    /*--------------------------------------------------------------------------------
        Settings
    --------------------------------------------------------------------------------*/
    [Header("Settings")]
    /// <summary>
    /// Total amount of points worth when hit.
    /// </summary>
    public int scorePoints;

    /*--------------------------------------------------------------------------------
        Private
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Holds current hole of mole.
    /// </summary>
    private Hole currentHole;

    /// <summary>
    /// Coroutine yield for auto disappear.
    /// </summary>
    private Coroutine coroutineHide;

    /// <summary>
    /// Coroutine for delayed reset.
    /// </summary>
    private Coroutine coroutineReset;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        SetHole
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Set hole of mole.
    /// </summary>
    /// <param name="hole">Hole object to set mole at.</param>
    public void SetHole(Hole hole)
    {
        currentHole = hole;
        currentHole.containsMole = true;

        Vector3 position = hole.transform.position;
        position.y = 0f;
        this.transform.position = position;
    }
    /*--------------------------------------------------------------------------------
        StartHideTimer
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Start hide timer coroutine.
    /// </summary>
    /// <param name="randomHideTimeMin"></param>
    /// <param name="randomHideTimeMax"></param>
    public void StartHideTimer(float randomHideTimeMin, float randomHideTimeMax)
    {
        float randomDuration = Random.Range(randomHideTimeMin, randomHideTimeMax);
        coroutineHide = StartCoroutine(HideTimer(randomDuration));
    }
    /*--------------------------------------------------------------------------------
        StopHideTimer
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Stop hide timer coroutine.
    /// </summary>
    public void StopHideTimer()
    {
        if (coroutineHide != null)
        {
            StopCoroutine(coroutineHide);
        }
    }
    /*--------------------------------------------------------------------------------
        Reset
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Reset mole to default state.
    /// </summary>
    /// <param name="delay">Delay before resetting.</param>
    public void Reset(float delay=0f)
    {
        if(coroutineReset != null)
        {
            StopCoroutine(coroutineReset);
        }

        coroutineReset = StartCoroutine(ResetDelayed(delay));
    }
    /*--------------------------------------------------------------------------------
        Appear
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Let mole appear.
    /// </summary>
    public void Appear()
    {
        capsuleCollider.enabled = true;
        animator.Play(AnimationType.Appear.ToString().ToLower());
    }
    /*--------------------------------------------------------------------------------
        Disappear
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Let mole disappear.
    /// </summary>
    public void Disappear()
    {
        capsuleCollider.enabled = false;
        animator.Play(AnimationType.Disappear.ToString().ToLower());
        Reset(0.30f);
    }
    /*--------------------------------------------------------------------------------
        Hit
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Hit a mole.
    /// </summary>
    public void Hit()
    {
        capsuleCollider.enabled = false;
        animator.Play(AnimationType.Hit.ToString().ToLower());
        Reset(0.15f);
    }

    /*################################################################################
        Function: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        HideTimer
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Coroutine for hiding mole after certain time duration.
    /// </summary>
    /// <param name="duration">Time until hiding mole</param>
    /// <returns></returns>
    private IEnumerator HideTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        Disappear();
    }
    /*--------------------------------------------------------------------------------
        ResetDelayed
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Coroutine to reset mole with a delay.
    /// </summary>
    /// <param name="delay">Delay in seconds</param>
    /// <returns></returns>
    private IEnumerator ResetDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        StopHideTimer();

        if (IsVisible)
        {
            currentHole.containsMole = false;
            currentHole = null;
        }

        this.transform.position = new Vector3(0, -10f, 0);
        animator.Play(AnimationType.Invisible.ToString().ToLower());
    }

    /*################################################################################
        Attribute
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        IsVisible
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Get mole current visibility state.
    /// </summary>
    public bool IsVisible
    {
        get
        {
            return (currentHole != null);
        }
    }
}
