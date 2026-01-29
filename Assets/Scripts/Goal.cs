using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    BoxCollider box;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Turtle>())
        {
            Race.Instance.FinishRace();
        }
    }
}
