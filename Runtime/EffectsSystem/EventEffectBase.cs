using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EventEffectBase : ScriptableObject
{

    [SerializeField]
    private string effectName = "";

    public string EffectName => effectName;

    public EffectsContext Context { get; private set; }

    public abstract EffectsContext GenerateContextScheme();

    public abstract void ApplyToContext();

    public EventEffectBase()
    {
        Context = GenerateContextScheme();
    }

}
