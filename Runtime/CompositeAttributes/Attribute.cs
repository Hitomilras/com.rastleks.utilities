using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rastleks.utilities.Attributes
{

    [System.Serializable]
    public class Attribute : AttributeBase, IEffectsContainer
    {

        public float AttrValue { get { return Calculate(); } }

        public float AttrMultiplyer { get { if (isDirty) Calculate(); return attrMultiplyer; } }

        protected float attrValue;

        protected float attrMultiplyer;

        protected List<AttributeEffect> childern = new List<AttributeEffect>();

        protected bool isDirty = false;

        public event System.Action OnRecalculated;

        public Attribute(string name, float initialValue = 0) : base(name, initialValue)
        {
            attrValue = AttrBaseValue;
        }

        public virtual void Add(AttributeEffect attr)
        {
            childern.Add(attr);
            isDirty = true;
        }

        public virtual float Calculate()
        {
            if (isDirty)
            {
                attrValue = 0;
                attrMultiplyer = 0;

                foreach (var attr in childern)
                {
                    attrValue += attr.AttrBaseValue;
                    attrMultiplyer += attr.EffectMultiplyer;
                }

                attrValue = (AttrBaseValue + attrValue) * (1 + attrMultiplyer);

                OnRecalculated?.Invoke();
                isDirty = false;
            }

            return AttrValue;
        }

        public virtual void Remove(AttributeEffect attr)
        {
            childern.Add(attr);
            isDirty = true;
        }

        public AttributeEffect GetEffectByName(string name)
        {
            int nameHash = name.GetHashCode();

            foreach (var effect in childern)
                if (effect.AttrHash == nameHash)
                    return effect;

            return null;
        }
    }

}