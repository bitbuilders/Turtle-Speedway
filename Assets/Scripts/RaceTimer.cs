using System;
using UnityEngine;

public class RaceTimer : Subsystem
{
    public float RaceTime { get; private set; }

    void Start()
    {

    }

    void Update()
    {
        if (Race.Instance.State != RaceState.Racing)
            return;

        RaceTime += Time.deltaTime;
    }
}
