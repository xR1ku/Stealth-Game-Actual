using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavMeshRoamingAI : MonoBehaviour
{
    public float roamRadius = 10f; // Radius within which the AI roams
    public float waitTime = 3f; // Time to wait at each position
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Roam());
    }

    IEnumerator Roam()
    {
        while (true)
        {
            // Set a random destination within the roam radius
            Vector3 randomDestination = GetRandomPosition();
            agent.SetDestination(randomDestination);

            // Wait until the agent reaches its destination
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // Wait for a bit before choosing the next position
            yield return new WaitForSeconds(waitTime);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random point within the radius
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);

        // Return a valid position on the NavMesh
        return hit.position;
    }
}
