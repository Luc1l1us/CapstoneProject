using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    Aicontroller controller;
    public Waypoint currentWaypoint;

    int direction;

    void Awake()
    {
        controller = GetComponent<Aicontroller>();
    }

    void Start()
    {
        direction = Random.Range(0, 2);
        SetDestination();
    }

    void Update()
    {
        if (currentWaypoint == null)
        {
            // If the current waypoint is null, destroy the GameObject
            Destroy(gameObject);
            return; // exit the Update method to prevent further execution
        }

        UpdateNextDes();
    }

    void SetDestination()
    {
        if (currentWaypoint != null)
        {
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }

    public void UpdateNextDes()
    {
        if (controller.reachedDestination == true)
        {
            NextDes();
            SetDestination();
            Debug.Log("Updatingggg33333" + this.gameObject.name);
        }
    }
    private void NextDes()
    {
        bool shouldBranch = false;

        if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
        {
            shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio;
        }

        if (shouldBranch)
        {
            currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
        }
        else
        {
            if (direction == 0 && currentWaypoint.nextWaypoint != null)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
        }
    }


}
