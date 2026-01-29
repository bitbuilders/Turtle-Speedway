using System;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public string StartTag = "Start";

    public Transform GetStartTransform()
    {
        Collider startCollider = GetComponentInChildren<Collider>();

        return startCollider.transform;
    }

    void Start()
    {
    }
}
