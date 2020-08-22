using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles touch input for raycasting moles.
/// </summary>
public class InputManager : MonoBehaviour, ILoop
{
    /*################################################################################
        Variables
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    [Header("References")]
    /// <summary>
    /// Current camera to cast raycasts from.
    /// </summary>
    public Camera camera;

    /*--------------------------------------------------------------------------------
        References
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Triggered whenever a mole is hit with a screen touch.
    /// </summary>
    public Action<Mole> OnHit;

    /*################################################################################
        Functions: public
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        Loop
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Update per frame (Unity Update alternative).
    /// </summary>
    public void Loop()
    {
        TouchRaycastMole();
    }

    /*################################################################################
        Functions: private
    ################################################################################*/
    /*--------------------------------------------------------------------------------
        TouchRaycastMole
    --------------------------------------------------------------------------------*/
    /// <summary>
    /// Touch raycast mole, trigger event whenever a mole is hit.
    /// </summary>
    private void TouchRaycastMole()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = camera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Mole mole = hit.collider.GetComponent<Mole>();
                        if (mole != null)
                        {
                            OnHit?.Invoke(mole);
                        }
                    }
                }
            }
        }
    }
}
