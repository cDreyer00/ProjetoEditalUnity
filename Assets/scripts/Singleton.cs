using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T _instance;
    public static T Instance => _instance;

    [SerializeField] bool dontDestroyOnLoad;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;

        if(dontDestroyOnLoad)
            DontDestroyOnLoad(this);
    }
}
