using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    private PlayerMovementCC _playerMovementCC;

    /// <summary>
    /// Called when the script is enabled.
    /// If the PlayerMovementCC has not been found yet, it will be found by tag.
    /// The RotateArrow method will be added as a listener to the PlayerMovementCC's OnPlayerRotationChange event.
    /// </summary>
    void OnEnable()
    {
        if (_playerMovementCC == null)
        {
            _playerMovementCC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementCC>();
        }
        
        if (_playerMovementCC != null) { _playerMovementCC.OnPlayerRotationChange += RotateArrow; }
    }


    void OnDisable()
    {
        _playerMovementCC.OnPlayerRotationChange -= RotateArrow;
    }

    /// <summary>
    /// Rotates the arrow indicator to the specified angle.
    /// </summary>
    /// <param name="angle">The angle in degrees to rotate the arrow around the Z axis.</param>
    void RotateArrow(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
