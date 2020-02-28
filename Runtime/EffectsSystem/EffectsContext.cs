using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsContext
{

    public List<EffectsContextElement> Value = new List<EffectsContextElement>();

}

public class EffectsContextElement
{

    public string name;

    private object elementReference;

    public System.Type elementType;

    public T Get<T>()
    {
        if (elementReference != null && elementReference is T)
            return (T)elementReference;
        else
            throw new System.Exception("Invalid object  reference or  null");
    }

    public void Set(object obj)
    {
        if (obj != null && (obj.GetType() == elementType || obj.GetType().IsSubclassOf(elementType)))
            elementReference = obj;
        else
            throw new System.Exception("Invalid object type");
    }
}