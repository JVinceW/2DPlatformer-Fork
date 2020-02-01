using App.Scripts.Common;
using GameSystem.Common;
using GameSystem.Core.Interfaces;
using UnityEngine;

namespace GameSystem.Core {
    /// <summary>
    /// This class act as entry point of the game. This have to attach in the very first scene of the application (i.e title scene,...)
    /// </summary>
    public class ApplicationManager : MonoBehaviourSingleton<ApplicationManager> {
        [SerializeField]
        private float _defaultTimeScale = 1f;

        [Range(0, 10)]
        [SerializeField]
        private float _timeScale;

        [SerializeField]
        private string _topScenePath = "";

        private void Start() {
            var components = FindObjectsOfType<MonoBehaviour>();
            foreach (var component in components) {
                var callback = component as IOnAppLaunch;
                callback?.OnAppLaunch();
            }
        }

        private void OnApplicationPause(bool pauseStatus) {
            var components = FindObjectsOfType<MonoBehaviour>();
            foreach (var component in components) {
                if (pauseStatus) {
                    var callback = component as IOnAppSuspend;
                    callback?.OnAppSuspend();
                } else {
                    var callback = component as IOnAppResume;
                    callback?.OnAppResume();
                }
            }
        }

        private void Update() {
            if (!Mathf.Approximately(Time.timeScale, _timeScale)) {
                Time.timeScale = _timeScale;
            }
        }

        private void OnApplicationQuit() {
            var components = FindObjectsOfType<MonoBehaviour>();
            foreach (var component in components) {
                var callbackType = component as IOnAppDestroy;
                callbackType?.OnAppDestroy();
            }
        }

        /// <summary>
        /// Setup game timescale
        /// </summary>
        /// <param name="timeScale">TimeScale値</param>
        public void SetUpAppTimeScale(float timeScale) {
            _timeScale = timeScale;
        }

        /// <summary>
        /// Reset game timescale
        /// </summary>
        public void ResetTimeScale() {
            Time.timeScale = _defaultTimeScale;
        }
    }
}