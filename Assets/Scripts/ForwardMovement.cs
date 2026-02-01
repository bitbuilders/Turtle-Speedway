using System;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField]
    float MaxSpeed = 10.0f;

    [SerializeField]
    float MinSpeed = 3.0f;

    [SerializeField]
    float Acceleration = 3.0f;

    [SerializeField]
    float MaxBoostSpeed = 5.0f;

    [SerializeField]
    float SpeedFactor = 100.0f;

    [SerializeField]
    float SplineAlignmentSpeed = 10.0f;

    public float Boost
    {
        get => CurrentBoost;
        set => CurrentBoost = Mathf.Clamp(value, 0.0f, MaxBoostSpeed);
    }

    Vector3 Forward = Vector3.forward;
    Vector3 targetForward = Vector3.forward;

    float CurrentBoost = 0.0f;

    float Speed = 0.0f;

    Rigidbody body;

    Spline spline;

    void Awake()
    {
        body = GetComponent<Rigidbody>();

        spline = FindFirstObjectByType<Spline>();

        Speed = MinSpeed;
    }

    void Update()
    {
        Speed += Acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed + Boost);

        targetForward = spline.GetDir(transform.position);
        Forward = Vector3.Lerp(Forward, targetForward, Time.deltaTime * SplineAlignmentSpeed);

        transform.forward = Forward;
    }

    void FixedUpdate()
    {
        body.AddForce(Forward * Speed * SpeedFactor * Time.deltaTime);

        // print(body.linearVelocity.magnitude);
    }

    public void TakeHit(float speedReduction, float force)
    {
        Speed -= speedReduction;
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed + Boost);

        var f = -transform.forward * force;
        body.AddForce(f, ForceMode.Impulse);
    }
}
