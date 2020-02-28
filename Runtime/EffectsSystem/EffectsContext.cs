using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsContext
{
    /// <summary>
    /// Key for description, value for  type
    /// </summary>
    public Dictionary<string, System.Type> ContextScheme;

    /// <summary>
    /// Given context
    /// </summary>
    public List<object> Value;

}
