using UnityEngine;

namespace App.Scripts.Systems.PopupSystem {
    public class TestPopupBase : PopupBase {
        public override void CreatePopup(PopupParameterBase popupParam) {
            base.CreatePopup(popupParam);
            Debug.Log($"Created: {gameObject.name}");
        }
    }
}