using System;
using GameSystem.Common.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameSystem.Common {
    /// <summary>
    /// Single Mono behaviour base class.
    /// This system required Addressable package. If this singleton use with the <see cref="PrefabSingletonAttribute"/> you should check the MIsReady flag before call any method inside that instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        // ReSharper disable once StaticMemberInGenericType
        private static bool mShuttingDown;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once StaticMemberInGenericType
        private static object mLock = new object();
        private static T mInstance;

        // ReSharper disable once StaticMemberInGenericType
        /// <summary>
        /// Flag to check when init singleton from prefab. If true, this singleton is ready to use, otherwise the loading process still not finish
        /// </summary>
        public static bool MIsReady { get; private set; }

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance {
            get {
                if (mShuttingDown) {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }

                lock (mLock) {
                    if (mInstance == null) {
                        // Search for existing instance.
                        mInstance = (T) FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (mInstance == null) {
                            var type = typeof(T);
                            var hasPrefab = Attribute.IsDefined(type, typeof(PrefabSingletonAttribute));
                            if (hasPrefab) {
                                var prefAtt =
                                    Attribute.GetCustomAttribute(type,
                                        typeof(PrefabSingletonAttribute)) as PrefabSingletonAttribute;
                                CreateSingletonFromPrefab(prefAtt);
                            } else {
                                CreateNormalSingletonObj();
                            }
                        }
                    }

                    return mInstance;
                }
            }
        }

        /// <summary>
        /// Load Singleton game object from prefab
        /// </summary>
        /// <param name="attr">prefab attribute</param>
        private static GameObject CreateSingletonFromPrefab(PrefabSingletonAttribute attr) {
            GameObject prefab = null;
            Addressables.LoadAssetAsync<GameObject>(attr.PrefabPath).Completed += handle => {
                prefab = handle.Result;
                MIsReady = true;

                var singleton = Instantiate(prefab);
                mInstance = singleton.GetComponent<T>();
                singleton.name = !string.IsNullOrEmpty(attr.Name)
                    ? $"[Singleton]{attr.Name}"
                    : $"[Singleton]{singleton.name.Replace("(Clone)", "")}";

                // Make instance persistent.
                DontDestroyOnLoad(singleton);
            };
            return prefab;
        }

        /// <summary>
        /// Create a normal object than add singleton component into it
        /// </summary>
        private static void CreateNormalSingletonObj() {
            // Need to create a new GameObject to attach the singleton to.
            GameObject singleton = new GameObject {name = $"[Singleton]{typeof(T)}"};
            mInstance = singleton.AddComponent<T>();
            // Make instance persistent.
            DontDestroyOnLoad(singleton);
            MIsReady = true;
        }

        private void OnApplicationQuit() {
            mShuttingDown = true;
        }

        private void OnDestroy() {
            mShuttingDown = true;
        }
    }
}