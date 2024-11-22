using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    void OnEnable()
    {
        // Subscribe to the OnPlayerRotationChange event so that we can update the arrow indicator's rotation when the player's rotation changes.
        PlayerMovementCC.OnPlayerRotationChange += RotateArrow;
    }


    void OnDisable()
    {
        // Unsubscribe from the OnPlayerRotationChange event when the component is disabled.
        // This helps prevent memory leaks by removing the reference to the RotateArrow method.
        PlayerMovementCC.OnPlayerRotationChange -= RotateArrow;
    }

    /// <summary>
    /// Rotate the arrow indicator to match the rotation of the player.
    /// </summary>
    /// <param name="angle">The angle in degrees to rotate the arrow indicator.</param>
    void RotateArrow(float angle)
    {
        // Rotate the arrow indicator to match the rotation of the player.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
