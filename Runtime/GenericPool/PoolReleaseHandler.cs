using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PoolReleaseHandler<T> : MonoBehaviour
{

    private UnityEvent OnRelease = new UnityEvent();

    public void Init(UnityEngine.Events.UnityAction onReleaseAction)
    {
        OnRelease.AddListener(onReleaseAction);
    }

    public void Release()
    {
        OnRelease.Invoke();
    }
}
