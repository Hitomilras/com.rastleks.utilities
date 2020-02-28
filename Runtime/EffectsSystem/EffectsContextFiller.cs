using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsContextFiller
{

    public readonly List<EffectsContextFillRule> fillRules = new List<EffectsContextFillRule>();

    public void FillContext(EffectsContext target)
    {
        foreach (var contextElement in target.Value)
        {
            var fillActionRule = fillRules.FirstOrDefault(p => p.FillTargetType == contextElement.elementType);

            if (fillActionRule != null)
                contextElement.Set(fillActionRule.GetObjectToFillFunc.Invoke());
        }
    }

}
