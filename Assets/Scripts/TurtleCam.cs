using System;
using UnityEngine;

public class TurtleCam : MonoBehaviour
{
    [SerializeField]
    float TrailDistance = 10.0f;

    [SerializeField]
    float HeightOffset = 10.0f;

    [SerializeField]
    float Pitch = 30.0f;

    Camera cam;

    Turtle turtle = null;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!turtle)
        {
            turtle = FindFirstObjectByType<Turtle>(FindObjectsInactive.Exclude);
        }

        if (!turtle)
            return;

        var targetForward = turtle.transform.forward;
        targetForward.y = 0.0f;
        targetForward.Normalize();
        var target = turtle.transform.position;
        target += -targetForward * TrailDistance + Vector3.up * HeightOffset;

        transform.position = target;

        transform.rotation = Quaternion.Euler(Pitch, 0.0f, 0.0f);
    }
}
