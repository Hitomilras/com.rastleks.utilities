using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rastleks.utilities.Attributes
{

    public class AttributeEffect : AttributeBase
    {

        public float EffectMultiplyer => effectMultiplyer;

        [SerializeField]
        protected float effectMultiplyer;

        public AttributeEffect(string name, float constantValue = 0, float constantMultiplyer = 0) : base(name, constantValue)
        {
            effectMultiplyer = constantMultiplyer;
        }

    }

}