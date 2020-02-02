using UnityEditor;
using UnityEditor.SceneManagement;

namespace AppDevelopment.Editor {
    public static class SceneShortcutMenu {
        [MenuItem("Develop/Scenes/MainScene")]
        public static void OpenMainScene() {
            EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        }
    }
}