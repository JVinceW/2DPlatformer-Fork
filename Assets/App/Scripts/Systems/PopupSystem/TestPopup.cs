using System;
using UnityEngine;

namespace App.Scripts.Systems.PopupSystem {
    public class TestPopup : MonoBehaviour {
        [SerializeField]
        private string m_testPopupName;
        private void Start() {
            var instance = PopupManager.Instance;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                PopupManager.Instance.InitPopupManager();
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                PopupManager.Instance.OpenPopup(m_testPopupName, null);
            }
        }
    }
}