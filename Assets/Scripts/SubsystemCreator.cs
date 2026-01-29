using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SubsystemCreator : MonoBehaviour
{
    public static SubsystemCreator Instance { get; private set; }

    Dictionary<Type, Subsystem> Subsystems;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        Subsystems = new Dictionary<Type, Subsystem>();

        Assembly assembly = typeof(Subsystem).Assembly;

        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (type.IsSubclassOf(typeof(Subsystem)))
            {
                var existing = FindAnyObjectByType(type);
                if (existing != null)
                {
                    Subsystems.Add(type, (Subsystem)existing);
                    continue;
                }

                CreateSubsystem(type);
            }
        }
    }

    public static T GetSubsystem<T>() where T : Subsystem
    {
        var type = typeof(T);

        if (Instance.Subsystems.TryGetValue(type, out var subsystem))
        {
            return (T)subsystem;
        }

        var existing = FindAnyObjectByType(type);
        if (existing == null)
        {
            return (T)Instance.CreateSubsystem(type);
        }

        Instance.Subsystems.Add(type, (Subsystem)existing);

        return (T)existing;
    }

    Subsystem CreateSubsystem(Type type)
    {
        var newSubsystem = new GameObject($"{type.Name} Subsystem", type);
        var subsystem = newSubsystem.GetComponent<Subsystem>();
        Subsystems.Add(type, subsystem);

        return subsystem;
    }
}
