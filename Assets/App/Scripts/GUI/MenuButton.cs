using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.GUI {
    public class MenuButton : MonoBehaviour {
        [SerializeField]
        private Button m_button;

        [SerializeField]
        private GameObject m_cursor;

        public Button Button => m_button;

        public void SetCursor(bool active) {
            m_cursor.SetActive(active);
        }
    }
}