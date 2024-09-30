using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detect key press for W, A, S, or D and play sound
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            PlaySound();
        }
    }

    // Method to play the sound
    public void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource component found!");
        }
    }
}
