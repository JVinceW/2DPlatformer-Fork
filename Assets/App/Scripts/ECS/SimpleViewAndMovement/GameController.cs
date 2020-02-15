using App.Scripts.ECS.SimpleViewAndMovement.Services;
using App.Scripts.ECS.SimpleViewAndMovement.Systems;
using App.Scripts.ECS.SimpleViewAndMovement.Systems.UnityServiceFeature;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement {
    public class GameController : MonoBehaviour {
        private Entitas.Systems m_systems;
        private Contexts m_contexts;
        private ServiceWrapper m_wrapper;

        private void Start() {
            m_contexts = Contexts.sharedInstance;
            m_wrapper = new ServiceWrapper(new UnityInputService(), new UnityCameraService());
            m_systems = CreateSystem(m_contexts);
            m_systems.Initialize();
        }

        private void Update() {
            m_systems.Execute();
            m_systems.Cleanup();
        }

        private Entitas.Systems CreateSystem(Contexts contexts) {
            return new Feature("Systems")
                .Add(new RegisterUnityServiceFeature(contexts, m_wrapper))
                .Add(new ViewSystemFeature(contexts))
                .Add(new InputSystemFeature(contexts))
                .Add(new MovementSystemFeature(contexts))
                .Add(new MultiDestroySystem(contexts));
        }
    }
}