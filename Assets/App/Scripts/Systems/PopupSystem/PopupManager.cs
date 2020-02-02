using System.Collections.Generic;
using System.Linq;
using GameSystem.Common;
using GameSystem.Common.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace App.Scripts.Systems.PopupSystem {
    [PrefabSingleton("Assets/Prefabs/Managers/PopupManager.prefab")]
    public class PopupManager : MonoBehaviourSingleton<PopupManager> {
        [SerializeField]
        private GameObject m_popupCanvas;

        [SerializeField]
        private Image m_preventTouchBack;

        private bool m_hasInit;

        public bool HasInit => m_hasInit;
        private const string MANAGER_LOG_TAG = "[PopupManager]";
        private const string POPUP_ASSET_LABEL = "popup";

        private Dictionary<string, GameObject> m_popupHistory = new Dictionary<string, GameObject>();
        private List<PopupBase> m_popupCached = new List<PopupBase>();

        public void InitPopupManager() {
            if (m_hasInit) {
                return;
            }
            Addressables.LoadAssetsAsync<GameObject>(POPUP_ASSET_LABEL, go => { }).Completed += handle => {
                m_popupCached = handle.Result.Select(x => x.GetComponent<PopupBase>()).ToList();
                Debug.unityLogger.Log(MANAGER_LOG_TAG, $"Finished init PopupManager. {handle.Result.Count}");
                m_hasInit = true;
            };
        }

        public void ClearCache() {
            m_popupCached.Clear();
        }

        /// <summary>
        /// Open Popup
        /// </summary>
        /// <param name="popupName">Name of the popup prefab (in case different prefab share same popupBase type will be buggy so we use the popup name instead of use popup type)</param>
        /// <param name="param">param when init the popup</param>
        public void OpenPopup(string popupName, PopupParameterBase param) {
            if (!HasInit) {
                Debug.unityLogger.Log(MANAGER_LOG_TAG,
                    "Popup Manager not ready. Call InitPopupManager() before open popup");
                return;
            }

            var prefab = m_popupCached.Find(x => x.name == popupName);
            if (prefab == null) {
                Debug.unityLogger.Log(MANAGER_LOG_TAG, $"Can not found this popup: {popupName}");
                return;
            }

            var popup = Instantiate(prefab);
            var o = popup.gameObject;
            o.name = $"[Clone]{prefab.name}";
            prefab.CreatePopup(param);
        }
    }
}