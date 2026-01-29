using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Rammus : MonoBehaviour
{
    Movement movement;
    ForwardMovement forwardMovement;

    void Awake()
    {
        movement = GetComponent<Movement>();
        forwardMovement = GetComponent<ForwardMovement>();

        movement.enabled = false;
        forwardMovement.enabled = false;
    }

    void Start()
    {
        Race.Instance.OnRaceStart.AddListener(OnRaceStart);
    }

    void OnRaceStart()
    {
        movement.enabled = true;
        forwardMovement.enabled = true;
    }
}
