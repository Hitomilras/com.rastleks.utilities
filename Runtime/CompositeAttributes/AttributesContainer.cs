using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rastleks.utilities.Attributes
{

    [System.Serializable]
    public class AttributesContainer : IAttributeContainer
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

        public Attribute GetAttrByName(string name)
        {
            int nameHash = name.GetHashCode();

            foreach (var attr in attributes)
                if (attr.AttrHash == nameHash)
                    return (Attribute)attr;

            return null;
        }

        public bool ApplyEffectToAttributeWithName(string name, AttributeEffect effect)
        {
            var attr = GetAttrByName(name);

            if (attr != null)
            {
                attr.Add(effect);
                return true;
            }
            else
                return false;
        }
    }

}