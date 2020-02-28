using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rastleks.utilities.Attributes
{

    public interface IAttributeContainer
    {

        Attribute GetAttrByName(string name);

        bool ApplyEffectToAttributeWithName(string name, AttributeEffect effect);

    }

}