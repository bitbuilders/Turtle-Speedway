using System;
using UnityEngine;

public class TurtleHitbox : MonoBehaviour
{
    [SerializeField]
    float InvulnerabilityTime = 0.5f;

    ForwardMovement forwardMovement;

    float lastHitTime = 0.0f;

    void Awake()
    {
        forwardMovement = GetComponentInParent<ForwardMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        var hurtbox = other.GetComponent<EnemyHurtbox>();
        if (hurtbox != null && Time.time - lastHitTime > InvulnerabilityTime)
        {
            forwardMovement.TakeHit(hurtbox.SpeedHit, hurtbox.KnockbackForce);
            lastHitTime = Time.time;
        }
    }
}
