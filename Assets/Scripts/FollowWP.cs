using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWp = 0;
    Graph g;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.5f, 0.5f); //positions the sensors in the center instead of below the car
    public float frontSideSensorPos = 0.3f; //Depends on the dimension of the car according to the tutorial
    public float frontSensorAngle = 30;
    public bool shouldStop = false;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;

        
        currentNode = wps [0];

        Invoke("GoToEnd", 2);
    }

    public void GoToStart()
    {
        g.AStar(currentNode, wps[0]);
        currentWp = 0;
    }
    public void GoToEnd()
    {
        g.AStar(currentNode, wps[0]);
        currentWp = 0;
    }

    private void Update()
    {
        Sensors();
    }

    // Update is called once per frame
    void LateUpdate()
    {
            if (g.pathList.Count == 0 || currentWp == g.pathList.Count)
                return;

            if (Vector3.Distance(g.pathList[currentWp].getId().transform.position, this.transform.position) < accuracy)
            {
                currentNode = g.pathList[currentWp].getId();
                currentWp++;
            }

            if(currentWp < g.pathList.Count)
            {
                goal = g.pathList[currentWp].getId().transform;
                Vector3 lookatGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
                Vector3 direction = lookatGoal - this.transform.position;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

                this.transform.Translate(0, 0, speed * Time.deltaTime);
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
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("A*Car"))
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
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("A*Car"))
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
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("AICar") || hit.collider.CompareTag("A*Car"))
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

        if (carShouldStop == false)
        {
            speed = 2f;
        }
        else
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            speed = 0f;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
