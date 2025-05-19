/*
 * Author: Lee Kang Hao
 * Date: 17 Nov 2024
 * Description: This script handles the functionality of a button to open a specific wiki URL in the default browser.
 */

using UnityEngine;
using UnityEngine.UI;

public class WikiButtonHandler : MonoBehaviour
{
    [Header("Wiki Settings")]
    [Tooltip("URL of the wiki page to open when the button is clicked.")]
    public string wikiUrl = "https://yourwikiurl.com"; // Default URL, customizable in the Inspector

    private Button wikiButton; // Reference to the Button component attached to the GameObject

    void Start()
    {
        // Get the Button component from the GameObject this script is attached to
        wikiButton = GetComponent<Button>();

        // Add the OpenWiki method as a listener to the button's onClick event
        if (wikiButton != null)
        {
            wikiButton.onClick.AddListener(OpenWiki);
        }
        else
        {
            Debug.LogError("No Button component found on the GameObject.");
        }
    }

    // Method to open the wiki URL
    void OpenWiki()
    {
        if (!string.IsNullOrEmpty(wikiUrl)) // Check if the URL is not null or empty
        {
            Application.OpenURL(wikiUrl); // Open the URL in the default web browser
            Debug.Log("Opening Wiki URL: " + wikiUrl);
        }
        else
        {
            Debug.LogWarning("Wiki URL is empty. Please assign a valid URL in the Inspector.");
        }
    }
}
