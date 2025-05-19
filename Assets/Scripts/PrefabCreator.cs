/*
 * Author: Lee Kang Hao
 * Date: 17 Nov 2024
 * Description: This script uses ARFoundation to detect tracked images and spawn a prefab (e.g., "Iron Giant") 
 *              at the location of the detected image. The prefab's position can be adjusted using an offset.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject irongiant;  // The prefab to instantiate when an image is tracked.
    [SerializeField] private Vector3 prefablocation; // Position offset to apply when spawning the prefab.

    private ARTrackedImageManager aRTrackedImageManager; // Manages AR tracked images.

    private void OnEnable()
    {
        // Get the ARTrackedImageManager component from the GameObject this script is attached to.
        aRTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();

        // Subscribe to the trackedImagesChanged event, which is called when tracked images are added, updated, or removed.
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    // Called when the trackedImagesChanged event is triggered.
    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // Loop through the images that were newly added to tracking.
        foreach (ARTrackedImage image in obj.added)
        {
            // Instantiate the prefab at the location of the tracked image.
            irongiant = Instantiate(irongiant, image.transform);

            // Apply the specified offset to the prefab's position.
            irongiant.transform.position += prefablocation;
        }
    }
}
