using App.Scripts.Systems.PopupSystem.Interfaces;
using UnityEngine;

namespace App.Scripts.Systems.PopupSystem {
    public class PopupBase<TParam> : MonoBehaviour, IOnOpenPopup, IOnOpenedPopup, IOnClosePopup, IOnClosedPopup
        where TParam : class, new() {
        [SerializeField]
        private Animator m_popupAnimator;

        private readonly int m_openTrigger = Animator.StringToHash("Open");
        private readonly int m_closeTrigger = Animator.StringToHash("Close");

        public virtual void CreatePopup(TParam popupParam) { }

        public void Open() { }
        public void Close() { }

        public void OnClickNativeBackButton() {
            Close();
        }

        #region Callback Implementation
        public virtual void OnOpenPopup() { }

        public virtual void OnOpenedPopup() { }

        public virtual void OnClosePopup() { }

        public virtual void OnClosedPopup() { }
        #endregion
    }

    public class PopupBase : PopupBase<PopupParameterBase> { }
}