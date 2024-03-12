using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoSingleton<T>
{
    protected static T instance;

    public static bool IsInstantiated = false;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogWarningFormat("Could not found MonoSingleton Class on Singleton Object ({0}).", typeof(T).FullName);

                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    IsInstantiated = true;
                    Debug.LogWarningFormat("System auto-generated a new Singleton Object ({0}) to prevent runtime error.", typeof(T).FullName);
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = (this as T);
        IsInstantiated = true;
    }

    protected virtual void OnDestroy()
    {
        instance = null;
        IsInstantiated = false;
    }
}