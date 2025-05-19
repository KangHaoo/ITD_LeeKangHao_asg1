/*
 * Author: Lee Kang Hao
 * Date: 8 Nov 2024
 * Description: this script helps to manage the interaction with my track image and spawn object
 */



using UnityEngine;

public class InteractAndRevealChild : MonoBehaviour
{
    public string objectATag = "Tag1";  // Tag for Object A
    public string objectBTag = "Kirito";  // Tag for Object B
    public string childObjectName = "Sword"; // Name of the child object (Object C) within Object B

    private void OnTriggerEnter(Collider other)
    {
        // Check if this object is Object A and the other object is Object B
        if (gameObject.CompareTag(objectATag) && other.CompareTag(objectBTag))
        {
            // Find the child (Object C) within Object B
            Transform childToReveal = other.transform.Find(childObjectName);

            if (childToReveal != null)
            {
                // Enable Object C by setting it active
                childToReveal.gameObject.SetActive(true);

                // Destroy Object A
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Child object not found in Object B.");
            }
        }
    }
}
