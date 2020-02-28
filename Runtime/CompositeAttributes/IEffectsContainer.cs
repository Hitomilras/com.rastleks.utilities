using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rastleks.utilities.Attributes
{

    public interface IEffectsContainer
    {
        AttributeEffect GetEffectByName(string name);
    }

}