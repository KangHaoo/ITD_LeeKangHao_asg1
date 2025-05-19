/*
 * Author: Lee Kang Hao
 * Date: 11 nov 2024
 * Description: this script helps to link to scoremanager. Once this script interact with the robot, it is able to play sound and destory itself
 */

using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager scoreManager;  // Reference to the ScoreManager script
    public AudioSource audioSource;    // Reference to the AudioSource component
    public AudioClip collectSound;     // The sound to play when the coin is collected

    void Start()
    {
        // Find the ScoreManager in the scene dynamically
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }

        // Ensure that the audioSource is set up
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned in the Inspector!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("robot"))  // Ensure the collider is the robot (player)
        {
            // Add score when the robot touches the coin
            scoreManager.AddScore(1); // Add 1 point for each coin collected

            // Create a temporary GameObject to play the sound
            GameObject popSoundObject = new GameObject("PopSound");
            AudioSource popAudioSource = popSoundObject.AddComponent<AudioSource>();
            popAudioSource.clip = collectSound; // Set the clip to the collect sound
            popAudioSource.Play(); // Play the sound immediately

            Destroy(popSoundObject, collectSound.length); // Destroy the temporary object after the sound finishes

            Destroy(gameObject); // Destroy the coin after it is collected
        }
    }
}
