using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PoolReleaseHandler : MonoBehaviour
{

    private UnityEvent OnRelease = new UnityEvent();

    /// <summary>
    /// Key is listener function
    /// Value is autoRemoveFunction
    /// </summary>
    private Dictionary<UnityAction, System.Action> autoRemoveActions = new Dictionary<UnityAction, System.Action>();

    public void RemoveListener(UnityEngine.Events.UnityAction listenerToRemove)
    {
        OnRelease.RemoveListener(listenerToRemove);
        autoRemoveActions.Remove(listenerToRemove);
    }

    /// <summary>
    /// Add listener to OnRelease event. Listener will be removed after first invokation by default.
    /// </summary> 
    public void AddListener(UnityEngine.Events.UnityAction onReleaseAction, bool removeListenerOnRelease = true)
    {
        OnRelease.AddListener(onReleaseAction);

        if (removeListenerOnRelease)
        {
            void removeAction() { OnRelease.RemoveListener(onReleaseAction); }
            autoRemoveActions.Add(onReleaseAction, removeAction);
        }
    }

    public void Release()
    {
        OnRelease.Invoke();

        foreach (var action in autoRemoveActions)
            action.Key.Invoke();

        autoRemoveActions.Clear();
    }
}
