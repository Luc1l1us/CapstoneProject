using UnityEngine;

public class AICarSpawner : MonoBehaviour
{
    public GameObject aiCarPrefab;
    public Transform[] spawnPoints;
    public int maxAICars = 10;
    public int aiCarCount = 0;

    private void Start()
    {
        aiCarCount = transform.childCount;
        // Delay the initial spawn and then repeat the spawning every 5 seconds.
        InvokeRepeating("SpawnSingleAICar", 2f, 2f);
    }

    private void Update()
    {
        aiCarCount = transform.childCount;
        // Check if the AI car count is below the maximum.
        if (aiCarCount >= maxAICars)
        {
            // Stop the spawning when the maximum is reached.
            CancelInvoke("SpawnSingleAICar");
            CarDestroyed();
        }
    }

    private void SpawnSingleAICar()
    {
        // Choose a random spawn point index.
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Check if there is still room to spawn a car.
        if (aiCarCount < maxAICars)
        {
            // Check for nearby cars before spawning.
            bool canSpawn = CanSpawnCar(spawnPoint.position);

            if (canSpawn)
            {
                // Instantiate an AI car at the chosen spawn point.
                GameObject newAICar = Instantiate(aiCarPrefab, spawnPoint.position, Quaternion.identity);

                // Make the new AI car a child of the AICarManager.
                newAICar.transform.parent = transform;

                // Increment the AI car count.
                aiCarCount++;

                // Set the currentWaypoint of the AI car to the next waypoint of the spawn point's waypoint.
                WaypointNavigator navigator = newAICar.GetComponent<WaypointNavigator>();
                if (navigator != null)
                {
                    Waypoint spawnWaypoint = spawnPoint.GetComponent<Waypoint>();
                    if (spawnWaypoint != null && spawnWaypoint.nextWaypoint != null)
                    {
                        navigator.currentWaypoint = spawnWaypoint.nextWaypoint;

                        // Face the AI car towards the next waypoint.
                        Vector3 lookDirection = spawnWaypoint.nextWaypoint.transform.position - newAICar.transform.position;
                        newAICar.transform.rotation = Quaternion.LookRotation(lookDirection);
                    }
                }
            }
        }
    }

    // Check for nearby cars before spawning.
    private bool CanSpawnCar(Vector3 spawnPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, 5f); // Adjust the radius as needed.

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("AICar")) // Assuming AICars have a tag "AICar".
            {
                // There is already a car nearby, so don't spawn a new one.
                return false;
            }
        }

        // No cars nearby, it's safe to spawn a new one.
        return true;
    }
    public void CarDestroyed()
    {
        aiCarCount--;

        // Resume spawning if the count is below the maximum.
        if (aiCarCount < maxAICars)
        {
            InvokeRepeating("SpawnSingleAICar", 0f, 5f);
        }
    }
}
