using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float SteeringStrength = 5.0f;

    [SerializeField]
    float SpeedFactor = 100.0f;

    // [SerializeField]
    // float MaxForwardInput = 0.1f;
    //
    // [SerializeField]
    // float MaxBackwardInput = 0.5f;

    Vector3 InputDirection = Vector3.zero;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue iv)
    {
        var input = iv.Get<float>();
        InputDirection = new Vector3(input, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        if (Mathf.Approximately(InputDirection.sqrMagnitude, 0.0f))
            return;

        var force = InputDirection * SteeringStrength * SpeedFactor * Time.deltaTime;
        // if (force.z > 0.0f) force.z *= MaxForwardInput;
        // else force.z *= MaxBackwardInput;

        body.AddForce(force);
    }
}
