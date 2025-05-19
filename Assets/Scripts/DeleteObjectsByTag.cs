/*
 * Author: Lee Kang Hao
 * Date: 9 Nov 2024
 * Description: this script manage the delete function in my script. It delete by tags
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjectsByTag : MonoBehaviour
{
    public List<string> tagsToDelete = new List<string>(); // List of tags to delete

    // Call this method to delete objects with the specified tags
    public void DeleteAllObjectsWithTags()
    {
        foreach (string tag in tagsToDelete)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in objectsToDelete)
            {
                Destroy(obj);
            }
        }

        Debug.Log("Deleted all objects with specified tags.");
    }

    // Optional: Attach this to a UI button or another event to trigger the deletion
    public void DeleteWithConfirmation()
    {
        if (tagsToDelete.Count > 0)
        {
            DeleteAllObjectsWithTags();
        }
        else
        {
            Debug.LogWarning("No tags specified for deletion!");
        }
    }
}
