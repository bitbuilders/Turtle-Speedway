using System;
using UnityEngine;


[ExecuteAlways]
public class SplinePointEditor : MonoBehaviour
{
    public Spline spline;

    Vector3 lastPosition = Vector3.zero;

    void Awake()
    {
        gameObject.tag = "EditorOnly";
    }

    void OnDrawGizmos()
    {
        if (!spline.ShowSplinePoints)
            return;

        Gizmos.color = Color.gray1;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }

    void OnDrawGizmosSelected()
    {
        if (!spline.ShowSplinePoints)
            return;

        Gizmos.color = Color.deepSkyBlue;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }

    void Update()
    {
        if (Application.isPlaying)
            return;

        const float tolerance = 0.01f;
        if ((lastPosition - transform.position).sqrMagnitude > Mathf.Pow(tolerance, 2.0f))
        {
            spline.UpdateSplinePosition(this, transform.position);

            lastPosition = transform.position;
        }
    }

    void OnDestroy()
    {
        spline.OnSplinePointDeleted(this);
    }
}
