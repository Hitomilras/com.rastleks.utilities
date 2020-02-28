using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsContextFiller
{

    public readonly List<EffectsContextFillRule> fillRules = new List<EffectsContextFillRule>();

    public void FillContext(EffectsContext target)
    {
        foreach (var contextElement in target.ContextScheme)
        {
            var fillActionRule = fillRules.FirstOrDefault(p => p.FillTargetType == contextElement.Value);

            if (fillActionRule != null)
                target.Context.Add(fillActionRule.GetObjectToFillFunc.Invoke());
            else
                target.Context.Add(null);
        }
    }

}
