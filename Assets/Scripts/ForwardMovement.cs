using System;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField]
    Vector3 Forward = Vector3.forward;

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

    public float Boost
    {
        get => CurrentBoost;
        set => CurrentBoost = Mathf.Clamp(value, 0.0f, MaxBoostSpeed);
    }

    float CurrentBoost = 0.0f;

    float Speed = 0.0f;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();

        Speed = MinSpeed;
    }

    void Update()
    {
        Speed += Acceleration * Time.deltaTime;
        Speed = Mathf.Clamp(MaxSpeed, MinSpeed, MaxSpeed + Boost);
    }

    void FixedUpdate()
    {
        body.AddForce(Forward * Speed * SpeedFactor * Time.deltaTime);

        print(body.linearVelocity.magnitude);
    }
}
