using UnityEngine;
using UnityEngine.AI;  // Assuming you're using NavMeshAgent for movement

public class GuardSight : MonoBehaviour
{
    public Transform player;  // The player object
    public float detectionRange = 10f;  // Max detection distance
    public float fieldOfViewAngle = 45f;  // Guard's field of view
    public NavMeshAgent guardNavMeshAgent;  // Guard's NavMeshAgent for movement
    private bool playerInSight = false;  // Whether the player is in sight

    private Vector3 originalDestination;  // Guard's original destination (for resuming patrol)

    void Start()
    {
        if (guardNavMeshAgent != null)
        {
            originalDestination = guardNavMeshAgent.destination;  // Store the original patrol destination
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the player is in the trigger zone
        if (other.transform == player)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Calculate the dot product to check if the player is in the field of view
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);
            float threshold = Mathf.Cos(fieldOfViewAngle * 0.5f * Mathf.Deg2Rad);

            // Check if the player is in front of the guard (within the field of view)
            if (dotProduct > threshold)
            {
                // Check if the player is within range
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer <= detectionRange)
                {
                    // Perform a raycast to check if there's a direct line of sight to the player
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
                    {
                        if (hit.transform == player)
                        {
                            playerInSight = true;
                            Debug.Log("Player detected! Stopping movement.");

                            // Stop the guard's movement when the player is in sight
                            if (guardNavMeshAgent != null)
                            {
                                guardNavMeshAgent.isStopped = true;  // Stop movement
                            }
                        }
                    }
                }
            }
            else
            {
                playerInSight = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player leaves the trigger zone, allow the guard to resume movement
        if (other.transform == player)
        {
            playerInSight = false;
            Debug.Log("Player out of sight. Resuming movement.");

            // Resume guard movement when the player is out of sight
            if (guardNavMeshAgent != null)
            {
                guardNavMeshAgent.isStopped = false;  // Resume movement
                guardNavMeshAgent.SetDestination(originalDestination);  // Continue patrolling
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw field of view in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRange);
    }
}
