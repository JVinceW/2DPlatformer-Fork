using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems.UnityServiceFeature {
    public class RegisterUnityCameraSystem : IInitializeSystem {
        private readonly Contexts m_contexts;
        private readonly ICameraService m_cameraService;
        public RegisterUnityCameraSystem(Contexts contexts, ICameraService cameraService) {
            m_contexts = contexts;
            m_cameraService = cameraService;
        }

        public void Initialize() {
            m_contexts.moveTutGame.ReplaceCameraService(m_cameraService);
        }
    }
}