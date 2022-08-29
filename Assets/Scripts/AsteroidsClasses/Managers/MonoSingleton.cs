using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ласс, €вл€ющим€ родительским дл€ всех объектов, которые нужно сделать синглтонами
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) UnityEngine.Debug.LogError("There's no " + typeof(T).ToString());
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogError("There's more than one " + typeof(T).ToString());
            return;
        }
        _instance = this as T;
    }
}