using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aicontroller : MonoBehaviour
{
    [Header ("For Waypoint")]
    public float movementSpeed = 2f;
    public float rotationSpeed = 45f;
    public float stopDistance = 2.5f;
    public bool reachedDestination = false;
    private float destroyTimer = 1f;
    Vector3 lastPosition;
    Vector3 velocity;
    public float destinationUpdateThreshold = 1.0f;
    public Vector3 destination;

    WaypointNavigator Navi;

    public float maxSpeed = 5f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float probabilityParameter = 0.1f;
    public float distance_to_next = 3f; // Distance to the next vehicle (adjust as needed)

    [Header ("A Star")]
    public Transform goal;
    public float accuracy = 1.0f;
    public GameObject wpManager;
    public GameObject currentNode;
    GameObject[] wps;
    int currentWp = 0;
    Graph g;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.5f, 0.5f); //positions the sensors in the center instead of below the car
    public float frontSideSensorPos = 0.3f; //Depends on the dimension of the car according to the tutorial
    public float frontSensorAngle = 30;
    public bool shouldStop = false;



    private void Start()
    {   
        if (this.CompareTag("Astar"))
        {
            wpManager = GameObject.FindWithTag("WPManager");
            wps = wpManager.GetComponent<WPManager>().waypoints;
            g = wpManager.GetComponent<WPManager>().graph;
        }
        if(this.CompareTag("AICar"))
        {
            Navi = GetComponent<WaypointNavigator>();
        }
    }
    void Update()
    {
        Sensors();

        if(this.CompareTag("AICar"))
        {
            ReachDestination();
        }      
    }
    private void LateUpdate()
    {
        if (this.CompareTag("Astar"))
        {
            moveOnAStar();
        }
    }

    public void GoToStart()
    {
        g.AStar(currentNode, wps[0]);
        currentWp = 0;
    }
    public void GoToEnd()
    {
        g.AStar(currentNode, wps[6]);
        currentWp = 0;
    }

    private void ReachDestination()
    {
        if (!reachedDestination)
        {
            Debug.Log("Updatingggg!!!!");
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0f;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                velocity = (transform.position - lastPosition) / Time.deltaTime;
                velocity.y = 0;
                var velocityMagnitude = velocity.magnitude;
                velocity = velocity.normalized;
                var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
                var rightDotProduct = Vector3.Dot(transform.right, velocity);
            }
            else
            {
                reachedDestination = true;

            }
        }
        else
        {
            destroyTimer -= Time.deltaTime;

            if (destroyTimer <= 0f)
            {
                // Time is up, destroy the game object
                Debug.Log("I got bugged and destroyed");
                Destroy(gameObject);
            }
        }
    }
    public void SetDestination(Vector3 destination)
    {
        // Check if the new destination is significantly different from the current one
        if (Vector3.Distance(destination, this.destination) > destinationUpdateThreshold)
        {
            this.destination = destination;
            reachedDestination = false;
        }
    }

    private void moveOnAStar()
    {
        if (g.pathList.Count == 0 || currentWp == g.pathList.Count)
            return;

        if (Vector3.Distance(g.pathList[currentWp].getId().transform.position, this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWp].getId();
            currentWp++;
        }

        if (currentWp < g.pathList.Count)
        {
            goal = g.pathList[currentWp].getId().transform;
            Vector3 lookatGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookatGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            this.transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }
    }
    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPos.z;
        sensorStartPos += transform.up * frontSensorPos.y;


        bool carShouldStop = shouldStop;


        //front center Sensor
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("Astar"))
                {
                    Debug.Log("Hit on front" + hit.collider.tag);
                    carShouldStop = true; // Set the flag to true to indicate a collision
                    Debug.DrawLine(sensorStartPos, hit.point);
                }
                else
                {
                    Debug.Log("false");
                    carShouldStop = false;      
                }
                Debug.DrawLine(sensorStartPos, hit.point);
            }
            
        }

        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPos;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("Astar"))
                {
                    Debug.Log("Hit fornt right" + hit.collider.tag);
                    carShouldStop = true; // Set the flag to true to indicate a collision
                    Debug.DrawLine(sensorStartPos, hit.point);
                }
                else
                {
                    Debug.Log("false");
                    carShouldStop = false;
                }
                Debug.DrawLine(sensorStartPos, hit.point);
            }
        }

        //front right angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
        }

        //front left sensor
        sensorStartPos -= transform.right * frontSideSensorPos * 2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("Astar"))
                {
                    Debug.Log("Hit on fornt left" + hit.collider.tag);
                    carShouldStop = true; // Set the flag to true to indicate a collision
                    Debug.DrawLine(sensorStartPos, hit.point);
                }
                else
                {
                    Debug.Log("false");
                    carShouldStop = false;
                }
                Debug.DrawLine(sensorStartPos, hit.point);
            }
        }

        //front left angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
            }
        }
        if (!carShouldStop)
        {
            movementSpeed = maxSpeed;
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            movementSpeed = 0f;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
 }
