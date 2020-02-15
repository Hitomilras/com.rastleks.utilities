using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesContainer
{

    [SerializeReference]
    private List<AttributeBase> attributes = new List<AttributeBase>();

    public void Add(AttributeBase attr)
    {
        attributes.Add(attr);
    }

    public void Remove(AttributeBase attr)
    {

    }

    public void RemoveAt(int index)
    {
        attributes.RemoveAt(index);
    }

    public AttributeBase GetAttrByName(string name)
    {
        int nameHash = name.GetHashCode();

        foreach (var attr in attributes)
            if (attr.AttrHash == nameHash)
                return attr;

        return null;
    }
}
