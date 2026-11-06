using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmos : MonoBehaviour
{
    public Color gizmoColor = Color.red; // Set the color of the Gizmo in the Inspector.

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, .5f); // Draw a sphere at the GameObject's position.
    }
}
