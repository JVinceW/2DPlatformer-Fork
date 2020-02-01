using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        private MenuButton[] m_menuButtons;
        
        [Space]
        [SerializeField]
        private float m_btnSelectThreshold = 0.2f;

        [SerializeField]
        private GameObject m_optionMenu;

        [SerializeField]
        private Transform m_mainCanvas;

        [Header("Other Settings")]
        [SerializeField]
        private Text m_versionTxt;

        private MenuButton StartGameBtn => m_menuButtons[0];
        private MenuButton OptionBtn => m_menuButtons[1];
        private MenuButton ExitGameBtn => m_menuButtons[2];
        private int m_nowSelectingIdx = 0;

        private bool m_canSelect = true;

        private OptionController m_optionController;

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

        public void SetSelectingButton() {
            var eventData = EventSystem.current;
            foreach (var btn in m_menuButtons) {
                btn.Button.OnPointerExit(new PointerEventData(eventData));
                btn.SetCursor(false);
            }

            var selecting = m_menuButtons[m_nowSelectingIdx];
            selecting.Button.OnPointerEnter(new PointerEventData(eventData));
            selecting.SetCursor(true);
        }

        public void OnClickStartGame() {
            SceneManager.LoadSceneAsync(1);
        }

        public void OnClickOptions() {
            throw new NotImplementedException();
        }

        public void OnClickExitGame() {
            Application.Quit();
        }
    }
}