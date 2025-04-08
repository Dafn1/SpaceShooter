using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent] //мы не можем переставл€ть
public abstract class SingletoBase<T> : MonoBehaviour where T : MonoBehaviour //можем создать конкретный экземпл€р 
{

    public static T Instance { get; private set; }

    public void Init()
    {
        if (Instance != null)
        {
            Debug.LogWarning("MonoSingleton: object of type already exists, instance will be destroyed = " + typeof(T).Name);
            Destroy(this);
            return;
        }

        Instance = this as T;

    }


}
