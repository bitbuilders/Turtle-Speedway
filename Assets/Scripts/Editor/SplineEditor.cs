#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spline))]
public class SplineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        Spline spline = (Spline)target;

        // Add a button to the inspector
        if (GUILayout.Button(spline.IsDrawingSpline ? spline.stopSplineSegmentLabel : spline.addSplineSegmentLabel))
        {
            spline.ToggleSplineEditing();
        }

        if (GUILayout.Button("Clear Points"))
        {
            spline.ClearSplinePoints();
        }
    }
}

#endif