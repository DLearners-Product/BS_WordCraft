using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicates
        }
        else
        {
            instance = GetComponent<T>(); // Get component of type T attached to the same GameObject
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }
}