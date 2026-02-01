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

    [SerializeField]
    float YawSpeed = 1.0f;

    Camera cam;

    Turtle turtle = null;

    float yaw = 0.0f;
    float targetYaw = 0.0f;

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
        var target = turtle.transform.position;
        target += -targetForward * TrailDistance + Vector3.up * HeightOffset;

        transform.position = target;

        var turtleYaw = turtle.transform.rotation.eulerAngles.y;
        targetYaw = turtleYaw > 180.0f ? turtleYaw - 360.0f : turtleYaw;
        yaw = Mathf.Lerp(yaw, targetYaw, Time.deltaTime * YawSpeed);

        transform.rotation = Quaternion.Euler(Pitch, yaw, 0.0f);
    }
}
