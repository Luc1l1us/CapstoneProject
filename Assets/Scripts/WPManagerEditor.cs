#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(WPManager))]
public class WPManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WPManager wpManager = (WPManager)target;

        if (GUILayout.Button("Run Code"))
        {
            // Call the method you want to run when the button is clicked
    
        }
    }
}
