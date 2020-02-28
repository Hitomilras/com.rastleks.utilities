using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttributeContainer
{

    Attribute GetAttrByName(string name);

    bool ApplyEffectToAttributeWithName(string name, AttributeEffect effect);

}
