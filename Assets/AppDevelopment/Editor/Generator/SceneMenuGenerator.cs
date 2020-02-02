using UnityEditor;

namespace AppDevelopment.Editor.Generator {
    public static class SceneMenuGenerator {
        public static readonly string K_CLASS_TEMPLATE = @"using UnityEditor;
using UnityEditor.SceneManagement;

namespace AppDevelopment.Editor {
    public static class SceneShortcutMenu {
        $METHOD_ARRAY
    }
}";

        [MenuItem("Develop/Generator/Update Scene Menu")]
        public static void GenerateSceneMenu() { }
    }
}