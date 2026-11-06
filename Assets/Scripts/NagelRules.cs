using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NagelRules : MonoBehaviour
{
    [Header("Traffic Rules")]
    [Tooltip("Maximum speed for all vehicles")]
    public float maxSpeed = 5.0f;

    [Tooltip("Sight distance for vehicles to detect slower-moving vehicles")]
    public float sightDistance = 2.0f;

    [Tooltip("Probability for randomization")]
    [Range(0.0f, 1.0f)]
    public float probability = 0.1f;

    [Header("Vehicle Categories")]
    [Tooltip("AI Car Tag")]
    public string aiCarTag = "AICar";

    [Tooltip("A* Star Car Tag")]
    public string aStarCarTag = "Astar";

    public GameObject[] aiCars;
    public GameObject[] aStarCars;

    private void Start()
    {
        // Start the coroutine to periodically clean up destroyed cars
        StartCoroutine(CleanUpDestroyedCars());

        // Populate the arrays automatically based on tags
        aiCars = GetParentGameObjects(GameObject.FindGameObjectsWithTag(aiCarTag));
        aStarCars = GetParentGameObjects(GameObject.FindGameObjectsWithTag(aStarCarTag));
        UpdateVehicleArrays();
    }

    private void Update()
    {
        UpdateVehicleType(aiCars);
        UpdateVehicleType(aStarCars);
    }

    // Coroutine to periodically remove destroyed cars from arrays
    IEnumerator CleanUpDestroyedCars()
    {
        while (true)
        {
            // Remove destroyed cars from the arrays
            aiCars = RemoveDestroyedCars(aiCars);
            aStarCars = RemoveDestroyedCars(aStarCars);
            UpdateVehicleArrays();

            yield return new WaitForSeconds(.5f); // Adjust the interval as needed
        }
    }

    // Update the speed and behavior for a specific vehicle type
    void UpdateVehicleType(GameObject[] vehicles)
    {
        foreach (GameObject vehicle in vehicles)
        {
            if (vehicle == null) // Check for destroyed cars
                continue;

            Aicontroller aiController = vehicle.GetComponent<Aicontroller>();
            if (aiController != null)
            {
                // Rule A: Acceleration
                if (aiController.movementSpeed < maxSpeed)
                {
                    float accelerationRate = 10f; // Adjust the acceleration rate as needed
                    aiController.movementSpeed += accelerationRate * Time.deltaTime;
                    aiController.movementSpeed = Mathf.Max(aiController.movementSpeed, maxSpeed);

                }

                // Rule B: Deceleration
                foreach (GameObject otherVehicle in vehicles)
                {
                    if (otherVehicle != vehicle)
                    {
                        Aicontroller otherAiController = otherVehicle.GetComponent<Aicontroller>();
                        if (otherAiController != null)
                        {
                            float distance = Vector3.Distance(vehicle.transform.position, otherVehicle.transform.position);
                            if (distance < sightDistance && otherAiController.movementSpeed < aiController.movementSpeed)
                            {
                                aiController.movementSpeed = Mathf.Max(aiController.movementSpeed, otherAiController.movementSpeed - 1);
                            }
                        }
                    }
                }

                // Rule R: Randomization
                if (Random.value < probability)
                {
                    aiController.movementSpeed = Mathf.Max(aiController.movementSpeed - 1, 0);
                }
            }
        }
    }

    // Remove destroyed cars from the array
    GameObject[] RemoveDestroyedCars(GameObject[] vehicles)
    {
        return vehicles.Where(vehicle => vehicle != null).ToArray();
    }

    void UpdateVehicleArrays()
    {
        aiCars = GetParentGameObjects(GameObject.FindGameObjectsWithTag(aiCarTag));
        aStarCars = GetParentGameObjects(GameObject.FindGameObjectsWithTag(aStarCarTag));
    }

    GameObject[] GetParentGameObjects(GameObject[] childObjects)
    {
        List<GameObject> parentObjects = new List<GameObject>();

        foreach (GameObject childObject in childObjects)
        {
            Transform parentTransform = childObject.transform.parent;
            if (parentTransform != null)
            {
                GameObject parentObject = parentTransform.gameObject;
                if (!parentObjects.Contains(parentObject))
                {
                    parentObjects.Add(parentObject);
                }
            }
        }

        return parentObjects.ToArray();
    }
}
