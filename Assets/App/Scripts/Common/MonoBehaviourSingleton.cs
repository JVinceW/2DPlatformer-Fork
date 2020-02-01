using UnityEngine;

namespace App.Scripts.Common {
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        // ReSharper disable once StaticMemberInGenericType
        private static bool shuttingDown;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once StaticMemberInGenericType
        private static object lockObj = new object();
        private static T instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance {
            get {
                if (shuttingDown) {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }

                lock (lockObj) {
                    if (instance == null) {
                        // Search for existing instance.
                        instance = (T) FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (instance == null) {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T) + " (Singleton)";

                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return instance;
                }
            }
        }

        private void OnApplicationQuit() {
            shuttingDown = true;
        }

        private void OnDestroy() {
            shuttingDown = true;
        }
    } 
}