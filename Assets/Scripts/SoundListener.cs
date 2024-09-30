using UnityEngine;

public class SoundListener : MonoBehaviour  // Ensure it inherits from MonoBehaviour
{
    // Set the range of hearing
    public float hearingRange = 10f;

    // This method checks if the sound source is within hearing range
    public bool CanHearSound(Vector3 soundPosition, float soundVolume)
    {
        // Calculate the distance between the listener and the sound source
        float distance = Vector3.Distance(transform.position, soundPosition);

        // Calculate hearing threshold based on sound volume and range
        float effectiveRange = hearingRange * soundVolume;

        // Return true if within range
        return distance <= effectiveRange;
    }

    void Update()
    {
        // Example: Finding an object tagged as 'SoundSource'
        GameObject soundSource = GameObject.FindWithTag("SoundSource");

        if (soundSource != null)
        {
            // Assuming the soundSource has an AudioSource component attached
            AudioSource audioSource = soundSource.GetComponent<AudioSource>();

            if (audioSource != null && audioSource.isPlaying)
            {
                bool canHear = CanHearSound(soundSource.transform.position, audioSource.volume);

                if (canHear)
                {
                    Debug.Log("Hearing the sound");
                    // Add custom behavior when sound is heard
                }
                else
                {
                    Debug.Log("Out of range for sound");
                }
            }
        }
    }
}
