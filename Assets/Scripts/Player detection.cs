using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player;
    public Transform guardForward;
    public float detectionFOV = 60f; // Field of view angle

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - guardForward.position).normalized;
        Vector3 guardForwardDirection = guardForward.forward.normalized;

        float dotProduct = Vector3.Dot(guardForwardDirection, directionToPlayer);

        // Convert FOV to radians for comparison with dot product
        float halfFOVInRadians = Mathf.Cos(detectionFOV * 0.5f * Mathf.Deg2Rad);

        if (dotProduct >= halfFOVInRadians)
        {
            Debug.Log("Player is in sight!");
            // Logic to handle player detection
        }
    }
}
