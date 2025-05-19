/*
 * Author: Lee Kang Hao
 * Date: 9 Nov 2024
 * Description: This script handles interaction between two objects. When Object A (with this script) collides with 
 *              Object B (tagged with a specified tag), a part of a prefab is made visible, and Object A is destroyed.
 */

using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    // Reference to the part of the prefab that should be made visible (unhidden).
    [Tooltip("The part of the prefab to unhide when interaction occurs.")]
    public GameObject partToUnhide;

    // The tag that identifies the target object (Object B) this script interacts with.
    [Tooltip("Tag for the target object (Object B). Ensure Object B has this tag in the Unity Editor.")]
    public string targetTag = "Kirito";

    // Called when another collider enters the trigger collider attached to this GameObject.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger collider has the specified tag.
        if (other.CompareTag(targetTag))
        {
            // If partToUnhide is assigned, activate it to make it visible.
            if (partToUnhide != null)
            {
                partToUnhide.SetActive(true);
            }

            // Destroy the current GameObject (Object A) from the scene.
            Destroy(gameObject);
        }
    }
}

