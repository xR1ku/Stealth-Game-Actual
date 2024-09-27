using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange = 10f; // How far the sound can be "heard"
    public LayerMask listenerLayer; // Specify layers that should be able to hear

    public void EmitSound()
    {
        // Play sound using AudioSource
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Notify nearby listeners
        Collider[] listeners = Physics.OverlapSphere(transform.position, soundRange, listenerLayer);
        foreach (Collider listener in listeners)
        {
            listener.GetComponent<SoundListener>()?.OnSoundHeard(transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the range of the sound in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }
}
