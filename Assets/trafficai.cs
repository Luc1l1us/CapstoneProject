using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class trafficai : MonoBehaviour
{
    public GameObject[] vehicles;
    public Transform[] rightLaneWaypoints;
    public float vehicleSpeed = 5f;

    private int currentWaypoint = 0;

    void Update()
    {
        MoveVehicles();
    }

    void MoveVehicles()
    {
        foreach (GameObject vehicle in vehicles)
        {
            Transform targetWaypoint = rightLaneWaypoints[currentWaypoint];
            Vector3 moveDirection = (targetWaypoint.position - vehicle.transform.position).normalized;
            vehicle.transform.Translate(moveDirection * vehicleSpeed * Time.deltaTime);

            if (Vector3.Distance(vehicle.transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % rightLaneWaypoints.Length;
            }
        }
    }
}
