using System;
using UnityEngine;

public class TurtleMesh : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 20.0f;

    [SerializeField]
    float turnSpeed = 2.0f;

    [SerializeField]
    float maxYaw = 20.0f;

    [SerializeField]
    float maxRoll = 20.0f;

    float currentYaw = 0.0f;
    float currentRoll = 0.0f;
    float currentSteering = 0.0f;
    float currentPitch = 0.0f;

    Movement movement;

    void Awake()
    {
        movement = GetComponentInParent<Movement>();
    }

    void Update()
    {
        if (Race.Instance.State == RaceState.Countdown)
            return;

        // var currentPitch = transform.localRotation.eulerAngles.x;
        currentPitch += Time.deltaTime * rotationSpeed;
        currentPitch = currentPitch > 90.0f ? currentPitch - 180.0f : currentPitch;
        print(currentPitch);
        transform.localRotation =Quaternion.Euler(0.0f, currentYaw, -currentRoll) * Quaternion.Euler(currentPitch, 0.0f, 0.0f) ;
        // transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, currentYaw, -currentRoll);
        // transform.Rotate(transform.parent.right, Time.deltaTime * rotationSpeed);
    }

    void LateUpdate()
    {
        currentSteering = Mathf.Lerp(currentSteering, movement.steering, Time.deltaTime * turnSpeed);
        currentYaw = currentSteering * maxYaw;
        currentRoll = currentSteering * maxRoll;
    }
}
