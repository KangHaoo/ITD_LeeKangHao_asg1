/*
 * Author: Lee Kang Hao
 * Date: 14 Nov 2024
 * Description: A script to spawn and interact with figurines. The player can select a prefab, spawn it in the scene 
 *              by tapping on a surface, and drag the spawned objects by touching or clicking and moving them.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FigurineSpawner : MonoBehaviour
{
    // References to the prefab objects for spawning.
    public GameObject Prefab1;
    public GameObject Prefab2;
    public GameObject Prefab3;
    public GameObject Prefab4;
    public GameObject Prefab5;
    public GameObject Prefab6;
    public GameObject Prefab7;
    public GameObject Prefab8;
    public GameObject Prefab9;
    public GameObject Prefab10;

    private GameObject selectedPrefab;          // Stores the currently selected prefab for spawning.
    private GameObject currentDraggingObject;   // Tracks the currently dragged object.
    private Camera mainCamera;                 // Reference to the main camera.

    private void Start()
    {
        // Get the main camera reference.
        mainCamera = Camera.main;
    }

    // Method to select which prefab to spawn based on the input name.
    public void SelectObject(string objectName)
    {
        switch (objectName)
        {
            case "Prefab1":
                selectedPrefab = Prefab1;
                break;
            case "Prefab2":
                selectedPrefab = Prefab2;
                break;
            case "Prefab3":
                selectedPrefab = Prefab3;
                break;
            case "Prefab4":
                selectedPrefab = Prefab4;
                break;
            case "Prefab5":
                selectedPrefab = Prefab5;
                break;
            case "Prefab6":
                selectedPrefab = Prefab6;
                break;
            case "Prefab7":
                selectedPrefab = Prefab7;
                break;
            case "Prefab8":
                selectedPrefab = Prefab8;
                break;
            case "Prefab9":
                selectedPrefab = Prefab9;
                break;
            case "Prefab10":
                selectedPrefab = Prefab10;
                break;
            default:
                selectedPrefab = null;
                Debug.Log("No object selected");
                break;
        }

        if (selectedPrefab != null)
            Debug.Log(objectName + " selected");
    }

    private void Update()
    {
        HandleSpawn(); // Handle spawning of objects.
        HandleDrag();  // Handle dragging of spawned objects.
    }

    // Method to handle spawning objects at the touch/click location.
    private void HandleSpawn()
    {
        bool isTouching = false; // Indicates if the screen is being touched.
        Vector2 touchPosition = Vector2.zero; // Stores the touch/click position.

        // Check for touch or mouse click input.
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                isTouching = true;
                touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            }
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isTouching = true;
            touchPosition = Mouse.current.position.ReadValue();
        }

        // Spawn the selected prefab at the touch/click location if valid.
        if (isTouching && selectedPrefab != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject spawnedObject = Instantiate(selectedPrefab, hit.point, Quaternion.identity);

                // Assign unique tags to distinguish different prefabs.
                AssignTagToSpawnedObject(spawnedObject);

                // Reset the selected prefab after spawning.
                selectedPrefab = null;
            }
        }
    }

    // Assign a unique tag to the spawned object based on the selected prefab.
    private void AssignTagToSpawnedObject(GameObject spawnedObject)
    {
        if (selectedPrefab == Prefab1) spawnedObject.tag = "Tag1";
        else if (selectedPrefab == Prefab2) spawnedObject.tag = "Tag2";
        else if (selectedPrefab == Prefab3) spawnedObject.tag = "Tag3";
        else if (selectedPrefab == Prefab4) spawnedObject.tag = "Tag4";
        else if (selectedPrefab == Prefab5) spawnedObject.tag = "Tag5";
        else if (selectedPrefab == Prefab6) spawnedObject.tag = "Tag6";
        else if (selectedPrefab == Prefab7) spawnedObject.tag = "Tag7";
        else if (selectedPrefab == Prefab8) spawnedObject.tag = "Tag8";
        else if (selectedPrefab == Prefab9) spawnedObject.tag = "Tag9";
        else if (selectedPrefab == Prefab10) spawnedObject.tag = "Tag10";
    }

    // Handle dragging of objects in the scene.
    private void HandleDrag()
    {
        bool isTouching = false; // Indicates if the screen is being touched.
        Vector2 touchPosition = Vector2.zero; // Stores the touch/click position.

        // Check for touch or mouse drag input.
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            isTouching = true;
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            isTouching = true;
            touchPosition = Mouse.current.position.ReadValue();
        }

        if (isTouching)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the hit object is one of the spawned objects by tag.
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Tag1") ||
                        hit.collider.CompareTag("Tag2") ||
                        hit.collider.CompareTag("Tag3") ||
                        hit.collider.CompareTag("Tag4") ||
                        hit.collider.CompareTag("Tag5") ||
                        hit.collider.CompareTag("Tag6") ||
                        hit.collider.CompareTag("Tag7") ||
                        hit.collider.CompareTag("Tag8") ||
                        hit.collider.CompareTag("Tag9") ||
                        hit.collider.CompareTag("Tag10"))
                    {
                        currentDraggingObject = hit.collider.gameObject;
                    }
                }
            }

            // Move the dragged object to the new touch position.
            if (currentDraggingObject != null)
            {
                MoveObjectToTouchPosition(currentDraggingObject, touchPosition);
            }
        }
        else
        {
            // Reset dragging object when the input stops.
            currentDraggingObject = null;
        }
    }

    // Move the specified object to the given touch position in world space.
    private void MoveObjectToTouchPosition(GameObject obj, Vector2 touchPosition)
    {
        float objectZDistance = mainCamera.WorldToScreenPoint(obj.transform.position).z;
        Vector3 screenPosition = new Vector3(touchPosition.x, touchPosition.y, objectZDistance);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        obj.transform.position = worldPosition;
    }
}
