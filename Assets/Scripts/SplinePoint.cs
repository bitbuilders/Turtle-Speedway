using System;
using UnityEngine;


// [ExecuteAlways]
public class SplinePoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray1;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.deepSkyBlue;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }

    // void Update()
    // {
    //     if (Application.isPlaying)
    //         return;
    //
    //     const float tolerance = 0.01f;
    //     if ((lastPosition - transform.position).sqrMagnitude > Mathf.Pow(tolerance, 2.0f))
    //     {
    //         spline.UpdateSplinePosition(this, transform.position);
    //
    //         lastPosition = transform.position;
    //     }
    // }
}
