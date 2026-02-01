using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spline : MonoBehaviour
{
    [NonSerialized]
    public readonly string addSplineSegmentLabel = "Edit spline";
    [NonSerialized]
    public readonly string stopSplineSegmentLabel = "Stop editing";

    [SerializeField]
    GameObject editorSplinePointPrefab = null;

    List<Transform> points = new List<Transform>();

    [SerializeField]
    public bool ShowSplinePoints = true;

    public bool IsDrawingSpline { get; set; }

    public static Spline EditingSpline { get; set; }

    void Awake()
    {
        points = GetPoints();
    }

    List<Transform> GetPoints()
    {
        return GetComponentsInChildren<SplinePoint>().Select(p => p.transform).ToList();
    }

    public void ToggleSplineEditing()
    {
        IsDrawingSpline = !IsDrawingSpline;

        EditingSpline = IsDrawingSpline ? this : null;
    }

    public void AddSplinePoint(Vector3 point)
    {
#if UNITY_EDITOR
        var newPoint = (GameObject)PrefabUtility.InstantiatePrefab(editorSplinePointPrefab, transform);
        newPoint.transform.position = point;
        var p = newPoint.GetComponent<SplinePoint>();

        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
    }

    public void ClearSplinePoints()
    {
        foreach (var splinePoint in GetPoints())
        {
            if (!splinePoint)
                continue;
            DestroyImmediate(splinePoint.gameObject);
        }
#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
    }

    public Vector3 GetDir(Vector3 worldPoint)
    {
        if (points.Count < 2)
            return Vector3.forward;

        Vector3 closestDir = Vector3.zero;
        float closestDist = float.MaxValue;
        for (int i = 0; i < points.Count - 1; ++i)
        {
            Vector3 first = points[i].position;
            Vector3 second = points[i + 1].position;
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
        Gizmos.DrawLineStrip(GetPoints().Select(p => p.transform.position).ToArray(), false);
    }
}
