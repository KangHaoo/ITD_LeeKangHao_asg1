/*
 * Author: Lee Kang Hao
 * Date: 16 Nov 2024
 * Description: A script that interacts with Object B based on mappings between Object A's tags and specific child objects in Object B.
 */

using UnityEngine;

public class ZakuInteractor : MonoBehaviour
{
    [System.Serializable]
    public class TagChildMapping
    {
        [Tooltip("Tag for the interacting object (Object A).")]
        public string objectTag;  // Tag for the interacting object (Object A)

        [Tooltip("Name of the child to reveal in Object B.")]
        public string childName; // Name of the child in Object B to activate
    }

    [Header("Mappings of Tags and Child Names")]
    [Tooltip("Array of mappings between Object A's tags and Object B's child names.")]
    public TagChildMapping[] tagChildMappings; // Array of mappings between tags and child names

    [Header("Object B Settings")]
    [Tooltip("Tag for Object B (the interacting parent object).")]
    public string objectBTag = "Zaku"; // Tag identifying Object B

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object matches the tag for Object B
        if (other.CompareTag(objectBTag))
        {
            // Loop through each tag-child mapping to find a match
            foreach (TagChildMapping mapping in tagChildMappings)
            {
                // Check if this object's tag matches one in the mappings
                if (gameObject.CompareTag(mapping.objectTag))
                {
                    // Search for the child object in Object B's hierarchy
                    Transform childToReveal = other.transform.Find(mapping.childName);

                    if (childToReveal != null)
                    {
                        // Activate the child object
                        childToReveal.gameObject.SetActive(true);

                        // Optionally destroy this object after interaction
                        Destroy(gameObject);
                    }
                    else
                    {
                        // Log a warning if the child object is not found
                        Debug.LogWarning($"Child object '{mapping.childName}' not found in Object B ({other.name}).");
                    }

                    // Exit the loop as we found a matching tag
                    break;
                }
            }
        }
    }
}
