using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class WaypointConnectorWindow : EditorWindow
{
    private bool ascendingOrder = true;

    [MenuItem("Window/Waypoint Connector")]
    public static void ShowWindow()
    {
        GetWindow<WaypointConnectorWindow>("Waypoint Connector");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Connect Waypoints", EditorStyles.boldLabel);

        ascendingOrder = EditorGUILayout.Toggle("Ascending Order", ascendingOrder);

        if (GUILayout.Button("Connect Selected Waypoints"))
        {
            ConnectWaypoints();
        }
    }

    private void ConnectWaypoints()
    {
        // Get all selected objects in the hierarchy
        GameObject[] selectedObjects = Selection.gameObjects;

        // Filter and find Waypoint components from the selected objects
        List<Waypoint> waypoints = new List<Waypoint>();
        foreach (var selectedObject in selectedObjects)
        {
            Waypoint wp = selectedObject.GetComponent<Waypoint>();
            if (wp != null)
            {
                waypoints.Add(wp);
            }
        }

        // Sort waypoints based on their names (ascending or descending order)
        waypoints.Sort((wp1, wp2) =>
        {
            if (ascendingOrder)
                return wp1.gameObject.name.CompareTo(wp2.gameObject.name);
            else
                return wp2.gameObject.name.CompareTo(wp1.gameObject.name);
        });

        // Assign the next waypoints, excluding the last one
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            int nextIndex = (i + 1) % waypoints.Count;
            waypoints[i].SetNextWaypoint(waypoints[i], waypoints[nextIndex]);
        }

        // Mark the scene as dirty to save changes.
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
