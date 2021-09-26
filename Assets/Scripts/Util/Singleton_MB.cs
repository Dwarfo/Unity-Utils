using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_MB<T> : MonoBehaviour where T : Singleton_MB<T>
{
    private static T instance;
    public static T Instance
    {
        get 
        {
            if (!IsInitialized) 
            {
                instance = FindObjectOfType(typeof(T)) as T;
            }
            return instance; 
        }
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }


    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.Log("[Singleton] Trying to instanctiate a second instance of singleton class");
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
