using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Initialize the current waypoint to the first one in the list
        if (waypoints.Count > 0)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            MoveToWaypoint(targetWaypoint);
        }
        else
        {
            Debug.LogError("No waypoints assigned to the car!");
        }
    }

    void Update()
    {
        // Check if the car has reached the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);

        if (distance < 0.5f) // You can adjust this threshold as needed
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }
            targetWaypoint = waypoints[currentWaypointIndex];
            MoveToWaypoint(targetWaypoint);
        }
    }

    void MoveToWaypoint(Transform target)
    {
        // Rotate towards the target waypoint
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the target waypoint
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
