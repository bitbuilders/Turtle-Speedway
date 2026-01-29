using System;
using UnityEngine;

public class RammusCam : MonoBehaviour
{
    [SerializeField]
    float TrailDistance = 10.0f;

    [SerializeField]
    float HeightOffset = 10.0f;

    [SerializeField]
    float Pitch = 30.0f;

    Camera cam;

    Rammus rammus = null;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!rammus)
        {
            rammus = FindFirstObjectByType<Rammus>(FindObjectsInactive.Exclude);
        }

        if (!rammus)
            return;

        var targetForward = rammus.transform.forward;
        targetForward.y = 0.0f;
        targetForward.Normalize();
        var target = rammus.transform.position;
        target += -targetForward * TrailDistance + Vector3.up * HeightOffset;

        transform.position = target;

        transform.rotation = Quaternion.Euler(Pitch, 0.0f, 0.0f);
    }
}
