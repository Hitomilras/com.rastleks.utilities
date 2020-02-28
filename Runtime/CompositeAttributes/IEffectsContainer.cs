using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectsContainer 
{
    AttributeEffect GetEffectByName(string name);
}
