/*
 * Author: Lee Kang Hao
 * Date: 17 Nov 2024
 * Description: Spawns coins at random locations on detected AR planes when a button is pressed.
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class SpawnCoinsOnPlane : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Prefab for the coin to spawn.")]
    public GameObject coinPrefab; // Prefab to spawn as a coin
    [Tooltip("AR Plane Manager that tracks AR planes.")]
    public ARPlaneManager arPlaneManager; // ARPlaneManager responsible for tracking AR planes
    [Tooltip("UI Button that triggers coin spawning.")]
    public Button spawnButton; // Button to trigger spawning

    [Header("Settings")]
    [Tooltip("Number of coins to spawn on a plane.")]
    public int coinsToSpawn = 5; // Number of coins to spawn

    void Start()
    {
        // Add a listener to the spawn button to call SpawnCoins method when clicked
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnCoins);
        }
        else
        {
            Debug.LogWarning("Spawn Button is not assigned in the Inspector.");
        }
    }

    public void SpawnCoins()
    {
        // Check if necessary references are set
        if (arPlaneManager == null || coinPrefab == null)
        {
            Debug.LogError("ARPlaneManager or CoinPrefab is not set!");
            return;
        }

        // Get all currently detected AR planes
        List<ARPlane> planes = new List<ARPlane>();
        foreach (var plane in arPlaneManager.trackables)
        {
            planes.Add(plane);
        }

        // If no planes are detected, log a message and exit
        if (planes.Count == 0)
        {
            Debug.Log("No AR planes detected!");
            return;
        }

        // Select a random plane from the list
        ARPlane randomPlane = planes[Random.Range(0, planes.Count)];

        // Spawn the specified number of coins at random points on the selected plane
        for (int i = 0; i < coinsToSpawn; i++)
        {
            Vector3 randomPoint = GetRandomPointOnPlane(randomPlane);
            Instantiate(coinPrefab, randomPoint, Quaternion.identity); // Spawn a coin prefab
        }
    }

    // Get a random point on the boundary of the given AR plane
    Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        // Select a random point from the plane's boundary
        Vector2 randomBoundaryPoint = plane.boundary[Random.Range(0, plane.boundary.Length)];

        // Convert the local point to a world position
        Vector3 randomPoint = plane.transform.TransformPoint(new Vector3(randomBoundaryPoint.x, 0, randomBoundaryPoint.y));
        return randomPoint;
    }
}
