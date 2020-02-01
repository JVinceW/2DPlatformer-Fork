using System;

namespace GameSystem.Common.Attributes {
    /// <summary>
    /// <para>Attribute that attach to the mono behaviour, which is singleton and was load from addressable.
    /// Note: Load from addressable is the async process. I highly recommend not to use this attribute unless you have to load a singleton with some special setting on prefab
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabSingletonAttribute : Attribute {
        public string Name { get; }

        public string PrefabPath { get; }

        public PrefabSingletonAttribute(string name, string prefabPath) {
            Name = name;
            PrefabPath = prefabPath;
        }

        public PrefabSingletonAttribute(string prefabPath) {
            PrefabPath = prefabPath;
        }
    }
}