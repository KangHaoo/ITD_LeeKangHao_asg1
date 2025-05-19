/*
 * Author: Lee Kang Hao
 * Date: 15 Nov 2024
 * Description: Manages the player's score in a game. Updates the score when coins are collected and 
 *              displays it using TextMeshPro UI.
 */

using UnityEngine;
using TMPro; // Required to use TextMesh Pro features

public class ScoreManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Reference to the TextMesh Pro UI component that displays the score.")]
    public TextMeshProUGUI scoreText; // UI element to display the score

    private int score; // Tracks the player's current score

    void Start()
    {
        // Initialize the score to 0 at the start of the game
        score = 0;

        // Update the score text to reflect the initial score
        UpdateScoreText();
    }

    // Method to add points to the player's score
    // This should be called when the player collects a coin or achieves a scoring event
    public void AddScore(int points)
    {
        score += points; // Increment the score by the given points

        // Update the score display
        UpdateScoreText();
    }

    // Updates the UI text element to display the current score
    void UpdateScoreText()
    {
        // Update the TextMesh Pro text with the current score
        scoreText.text = "Score: " + score.ToString();
    }
}
