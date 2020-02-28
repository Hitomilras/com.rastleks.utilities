using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EventEffectBase : ScriptableObject
{

    [SerializeField]
    private string effectName = "";

    public string EffectName => effectName;

    public EffectsContext Context { get; private set; } = new EffectsContext();

    public abstract void GenerateContextScheme();

    public abstract void ApplyToContext();

    public EventEffectBase()
    {
        GenerateContextScheme();
    }

}
