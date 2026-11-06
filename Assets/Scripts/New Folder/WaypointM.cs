using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointM : MonoBehaviour
{
    public Transform[] waypoints; // Drag and drop your waypoints in the inspector.
    public float carSpeed = 5.0f; // Adjust this speed as needed.

    private int currentWaypointIndex = 0;

    private void Start()
    {
        // Initialize the car at the first waypoint.
        if (waypoints.Length > 0)
        {
            MoveCarToWaypoint(waypoints[currentWaypointIndex]);
        }
    }

    private void Update()
    {
        // Check if the car has reached the current waypoint.
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.2f)
        {
            // Move to the next waypoint.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            MoveCarToWaypoint(waypoints[currentWaypointIndex]);
        }
    }

    private void MoveCarToWaypoint(Transform targetWaypoint)
    {
        // Rotate the car to face the target waypoint (optional).
        Vector3 direction = targetWaypoint.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        // Move the car towards the target waypoint.
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, carSpeed * Time.deltaTime);
    }
}
