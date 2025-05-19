/*
 * Author: Lee Kang Hao
 * Date: 15 Nov 2024
 * Description: This script handles the movement of a GameObject using a joystick input. 
 *              It moves the GameObject in the direction specified by the joystick and rotates
 *              it to face the movement direction.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoyStick : MonoBehaviour
{
    [SerializeField] private float speed; // Speed of the GameObject's movement.

    private FixedJoystick fixedJoystick; // Reference to the FixedJoystick component.
    private Rigidbody rigidBody;         // Reference to the Rigidbody component of the GameObject.

    // Called when the GameObject becomes active.
    private void OnEnable()
    {
        // Find a FixedJoystick component in the scene and assign it to the variable.
        fixedJoystick = FindAnyObjectByType<FixedJoystick>();

        // Get the Rigidbody component attached to this GameObject.
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Called every physics frame (FixedUpdate is used for physics-related updates).
    private void FixedUpdate()
    {
        // Get horizontal and vertical input values from the joystick.
        float xVal = fixedJoystick.Horizontal;
        float yval = fixedJoystick.Vertical;

        // Create a movement vector based on joystick input.
        Vector3 movement = new Vector3(xVal, 0, yval);

        // Set the Rigidbody's velocity to move the GameObject in the specified direction at the specified speed.
        rigidBody.velocity = movement * speed;

        // Rotate the GameObject to face the direction of movement (if there is any movement).
        if (xVal != 0 && yval != 0)
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,                     // Keep the current X rotation.
                Mathf.Atan2(xVal, yval) * Mathf.Rad2Deg,     // Calculate Y rotation based on joystick input.
                transform.eulerAngles.z                      // Keep the current Z rotation.
            );
    }
}

