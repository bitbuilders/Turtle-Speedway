using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyColors colors;

    [SerializeField]
    Renderer body;
    [SerializeField]
    Renderer head;
    [SerializeField]
    Renderer jaw;

    void Awake()
    {
        var randomMaterial = colors.Materials[Random.Range(0, colors.Materials.Count)];
        body.material = randomMaterial;
        head.material = randomMaterial;
        jaw.material = randomMaterial;
    }
}
