using System;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [NonSerialized]
    public readonly string addSplineSegmentLabel = "Edit spline";
    [NonSerialized]
    public readonly string stopSplineSegmentLabel = "Stop editing";

    [SerializeField]
    GameObject editorSplinePointPrefab = null;

    [SerializeField]
    List<Vector3> points = new List<Vector3>();

    [SerializeField]
    List<SplinePointEditor> editorPoints = new List<SplinePointEditor>();

    [SerializeField]
    public bool ShowSplinePoints = true;

    public bool IsDrawingSpline { get; set; }

    public static Spline EditingSpline { get; set; }

    bool clearing = false;

    public void ToggleSplineEditing()
    {
        IsDrawingSpline = !IsDrawingSpline;

        EditingSpline = IsDrawingSpline ? this : null;
    }

    public void AddSplinePoint(Vector3 point)
    {
        points.Add(point);

        var newPoint = Instantiate(editorSplinePointPrefab, point, Quaternion.identity, transform);
        var editor = newPoint.GetComponent<SplinePointEditor>();
        editor.spline = this;
        editorPoints.Add(editor);
    }

    public void ClearSplinePoints()
    {
        clearing = true;
        points.Clear();
        foreach (var splinePointEditor in editorPoints)
        {
            if (!splinePointEditor)
                continue;
            DestroyImmediate(splinePointEditor.gameObject);
        }
        editorPoints.Clear();
        clearing = false;
    }

    public void UpdateSplinePosition(SplinePointEditor point, Vector3 newPosition)
    {
        points[editorPoints.IndexOf(point)] = newPosition;
    }

    public void OnSplinePointDeleted(SplinePointEditor point)
    {
        if (clearing)
            return;

        var index = editorPoints.IndexOf(point);
        if (index < 0)
            return;

        points.RemoveAt(index);
        // DestroyImmediate(editorPoints[index].gameObject);
        editorPoints.RemoveAt(index);
    }

    public Vector3 GetDir(Vector3 worldPoint)
    {
        if (points.Count < 2)
            return Vector3.forward;

        Vector3 closestDir = Vector3.zero;
        float closestDist = float.MaxValue;
        for (int i = 0; i < points.Count - 1; ++i)
        {
            Vector3 first = points[i];
            Vector3 second = points[i + 1];
            float rawDist = (second - first).sqrMagnitude;
            Vector3 dir = (second - first).normalized;
            Vector3 nearest = NearestPoint(worldPoint, first, dir);
            if ((nearest - first).sqrMagnitude > rawDist)
                nearest = (nearest - first).sqrMagnitude < (nearest - second).sqrMagnitude ? second : first;

            float dist = (nearest - worldPoint).sqrMagnitude;
            if (dist < closestDist)
            {
                closestDist = dist;
                closestDir = dir;
            }
        }

        return closestDir;
    }

    Vector3 NearestPoint(Vector3 world, Vector3 point, Vector3 dir)
    {
        var toWorld = world - point;
        var dot = Vector3.Dot(toWorld, dir);
        return point + dir * dot;
    }

    void OnDrawGizmos()
    {
        if (!ShowSplinePoints)
            return;

        Gizmos.color = Color.black;
        Gizmos.DrawLineStrip(points.ToArray(), false);
    }
}
