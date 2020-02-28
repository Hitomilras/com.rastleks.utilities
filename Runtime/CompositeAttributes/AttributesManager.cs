using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace rastleks.utilities.Attributes
{

    public sealed class AttributesManager
    {

        private static AttributesManager instance;

        public static AttributesManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AttributesManager();

                return instance;
            }
        }

        public readonly List<System.Type> AttributesTypes;

        public AttributesManager()
        {
            AttributesTypes = new List<System.Type>();
            ReloadTypes();
        }

        void ReloadTypes()
        {
            CleanTypes();

            var types = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => { return t.GetTypes(); })
                .Where(p => p.IsSubclassOf(typeof(AttributeBase)) && !p.IsAbstract);

            AttributesTypes.AddRange(types);
        }

        void CleanTypes()
        {
            AttributesTypes.Clear();
        }

#if UNITY_EDITOR

        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnEditorReload()
        {
            Instance.ReloadTypes();
        }

#endif

    }

}