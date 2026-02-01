using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;

    [SerializeField]
    Transform point1;

    [SerializeField]
    Transform point2;

    [SerializeField]
    Transform nextPoint;

    [SerializeField]
    float acceptanceRange = 0.2f;

    [SerializeField]
    float rotationSpeed = 2.0f;

    Vector3 currentDir = Vector3.zero;

    void Awake()
    {
        currentDir = transform.forward;

        point1.transform.parent = null;
        point2.transform.parent = null;
    }

    void Update()
    {
        var dir = nextPoint.position - transform.position;
        var dirNorm = dir.normalized;
        var velocity = dirNorm * speed;
        transform.position += velocity * Time.deltaTime;

        currentDir = Vector3.RotateTowards(currentDir, dirNorm, rotationSpeed * Time.deltaTime, 0.0f);
        transform.forward = currentDir;

        var distSqr = dir.sqrMagnitude;
        if (distSqr < Mathf.Pow(acceptanceRange, 2.0f))
        {
            nextPoint = nextPoint == point1 ? point2 : point1;
        }
    }
}
