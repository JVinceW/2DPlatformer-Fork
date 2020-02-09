using UnityEngine;

namespace App.Scripts.ECS {
    public class HelloWorldTest : MonoBehaviour {
        private Entitas.Systems m_systems;

        private void Start() {
            var context = Contexts.sharedInstance;
            m_systems = new Feature("Main Systems").Add(new TutorialFeature(context));
            m_systems.Initialize();
        }

        private void Update() {
            m_systems.Execute();
            m_systems.Cleanup();
        }
    }
}