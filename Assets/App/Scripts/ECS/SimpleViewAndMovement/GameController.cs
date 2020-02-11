using App.Scripts.ECS.SimpleViewAndMovement.Systems;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement {
    public class GameController : MonoBehaviour {
        private Entitas.Systems m_systems;
        private Contexts m_contexts;

        private void Start() {
            m_contexts = Contexts.sharedInstance;
            m_systems = CreateSystem(m_contexts);
            m_systems.Initialize();
        }

        private void Update() {
            m_systems.Execute();
            m_systems.Cleanup();
        }

        private static Entitas.Systems CreateSystem(Contexts contexts) {
            return new Feature("Systems")
                .Add(new ViewSystemFeature(contexts))
                .Add(new InputSystemFeature(contexts))
                .Add(new MovementSystemFeature(contexts))
                .Add(new MultiDestroySystem(contexts));
        }
    }
}