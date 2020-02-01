using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.GUI {
    public class MainMenuController : MonoBehaviour {
        [Header("Cursor Setting")]
        [SerializeField]
        private float m_cursorMargin;

        [SerializeField]
        private GameObject m_cursor;

        [Header("Menu Button Setting")]
        [SerializeField]
        private Button[] m_menuButtons;

        [SerializeField]
        private float m_btnSelectThreshold = 0.2f;

        private Button StartGameBtn => m_menuButtons[0];
        private Button OptionBtn => m_menuButtons[1];
        private Button ExitGameBtn => m_menuButtons[2];
        private int m_nowSelectingIdx = 0;

        private bool m_canSelect = true;

        private void Start() {
            SetSelectingButton();
            // We assume that this game wont be play by mouse so we will disable it on build
#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
#endif
        }

        private void Update() {
            SelectMenu();
        }

        private void SelectMenu() {
            // Double hit prevention
            if (!m_canSelect) {
                return;
            }

            var vertical = Input.GetAxis("Vertical");
            var isMoveMouse = !Mathf.Approximately(vertical, 0f);


            // Move selection
            if (vertical > 0f) {
                m_nowSelectingIdx -= 1;
            } else if (vertical < 0) {
                m_nowSelectingIdx += 1;
            }

            if (isMoveMouse) {
                StartCoroutine(ReactiveSelection());
                m_nowSelectingIdx = Mathf.Clamp(m_nowSelectingIdx, 0, m_menuButtons.Length - 1);
                SetSelectingButton();
            }
        }

        private IEnumerator ReactiveSelection() {
            m_canSelect = false;
            yield return new WaitForSeconds(m_btnSelectThreshold);
            m_canSelect = true;
        }

        private void MoveCursor() {
            
        }
        
        public void SetSelectingButton() {
            var eventData = EventSystem.current;
            foreach (var btn in m_menuButtons) {
                btn.OnPointerExit(new PointerEventData(eventData));
            }

            var selecting = m_menuButtons[m_nowSelectingIdx];
            selecting.OnPointerEnter(new PointerEventData(eventData));
        }

        public void OnClickStartGame() {
            Debug.Log("Clicked start game");
        }

        public void OnClickOptions() {
            Debug.Log("Clicked options");
        }

        public void OnClickExitGame() {
            Debug.Log("Clicked exit game");
        }
    }
}