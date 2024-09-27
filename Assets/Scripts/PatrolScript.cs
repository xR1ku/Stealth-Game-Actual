using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPointIndex = 0; // Track which point is next
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Move to the first point
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
    }

    void Update()
    {
        // Check if the agent has reached the current destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        // If no patrol points, do nothing
        if (patrolPoints.Length == 0)
            return;

        // Set the agent to go to the next point in the array
        agent.SetDestination(patrolPoints[currentPointIndex].position);

        // Choose the next point in the array as the destination, cycling back if necessary
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }
}
