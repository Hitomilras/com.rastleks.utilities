using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AttributeBase : ISerializationCallbackReceiver
{

    public string AttrName => name;

    public int AttrHash => nameHash;

    public float AttrBaseValue => attrBaseValue;

    [SerializeField]
    protected string name;

    [HideInInspector]
    [SerializeField]
    protected int nameHash;

    [SerializeField]
    protected float attrBaseValue;


    public AttributeBase(string name, float initialValue = 0)
    {
        this.name = name;
        nameHash = name.GetHashCode();

        attrBaseValue = initialValue;
    }

    /// <summary>
    /// Checks if attribute match anotherr attribute by name
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return GetHashCode() == obj.GetHashCode();
    }

    public override int GetHashCode()
    {
#if UNITY_EDITOR
        if (string.IsNullOrEmpty(name))
            Debug.LogError("Attribute have no name.");
#endif

        if (nameHash == 0)
            nameHash = name.GetHashCode();

        return nameHash;
    }


    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(AttrName))
            nameHash = AttrName.GetHashCode();
        else
        {
            name = "No name";
            nameHash = AttrName.GetHashCode();
        }
#endif
    }

    public void OnAfterDeserialize()
    {

    }
}
