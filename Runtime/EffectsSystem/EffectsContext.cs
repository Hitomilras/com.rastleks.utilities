using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EffectsContext
{

    public EffectsContext(IList<EffectsContextElement> context)
    {
        Value = new ReadOnlyCollection<EffectsContextElement>(context);
    }

    public ReadOnlyCollection<EffectsContextElement> Value;

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
        if (obj != null && (obj.GetType() == elementType || obj.GetType().IsAssignableFrom(elementType)))
            elementReference = obj;
        else
            throw new System.Exception("Invalid object type");
    }
}