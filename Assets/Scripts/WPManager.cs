using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction { UNI, BI }
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}


public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    public GameObject player;
    public GameObject mapPointer; // Dynamically assigned at runtime

    public LineRenderer pathRenderer; // Add a LineRenderer component to an empty GameObject and assign it in the Inspector.

    // Start is called before the first frame update
    void Start()
    {
        Wpoints();
        CreateLinks(Link.direction.BI);


    }
    private void CreateLinks(Link.direction dir)
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject wpObject in waypoints)
            {
                Waypoint wp = wpObject.GetComponent<Waypoint>();
                if (wp != null)
                {
                    graph.AddNode(wpObject);

                    Waypoint nextWaypoint = wp.nextWaypoint;
                    if (nextWaypoint != null)
                    {
                        // Create a link from the current waypoint to its next waypoint
                        graph.AddEdge(wpObject, nextWaypoint.gameObject);


                        // If bidirectional, add the reverse link
                        if (dir == Link.direction.BI)
                        {
                            graph.AddEdge(nextWaypoint.gameObject, wpObject);
                        }
                    }
                }
            }
        }
    }
    private void Update()
    {
        mapPointer = GameObject.FindWithTag("MapPointer"); // Dynamically find the map pointer object by its tag.

        if (mapPointer == null)
        {
            ClearPath();
            return;
        }

        // Find the nearest waypoints for the player and the map pointer.
        GameObject playerWaypoint = graph.FindNearestWaypoint(player.transform.position);
        GameObject mapPointerWaypoint = graph.FindNearestWaypoint(mapPointer.transform.position);

        if (playerWaypoint != null && mapPointerWaypoint != null)
        {
            // Use the A* algorithm to find the path between the nearest waypoints.
            bool pathFound = graph.AStar(playerWaypoint, mapPointerWaypoint);

            if (pathFound)
            {
                List<Node> path = graph.pathList;

                // Visualize the path by drawing lines.
                VisualizePath(path);
                Debug.Log("Path found from player to map pointer!");
            }
            else
            {
                Debug.Log("Path not found from player to map pointer.");
            }
        }
        else
        {
            Debug.Log("Could not find nearest waypoints for player or map pointer.");
        }

    }
    private void ClearPath()
    {
        if (pathRenderer != null)
        {
            pathRenderer.positionCount = 0;
        }
    }

    private void Wpoints()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
                foreach (Link l in links)
                {
                    graph.AddEdge(l.node1, l.node2);
                    if (l.dir == Link.direction.BI)
                    {
                        graph.AddEdge(l.node2, l.node1);
                    }
                }
            }

        }
    }

    void VisualizePath(List<Node> path)
    {
        if (pathRenderer == null)
        {
            Debug.LogWarning("PathRenderer is not assigned. Please assign a LineRenderer in the Inspector.");
            return;
        }

        int pathCount = path.Count;
        pathRenderer.positionCount = pathCount;

        for (int i = 0; i < pathCount; i++)
        {
            pathRenderer.SetPosition(i, path[i].getId().transform.position);
        }

        // If the path is shorter than the previous path, clear the remaining positions.
        for (int i = pathCount; i < pathRenderer.positionCount; i++)
        {
            pathRenderer.SetPosition(i, Vector3.zero);
        }

        // Add a final position to ensure the line reaches the last node.
        if (pathCount > 0)
        {
            pathRenderer.SetPosition(pathCount - 1, path[pathCount - 1].getId().transform.position);
        }
    }

}
