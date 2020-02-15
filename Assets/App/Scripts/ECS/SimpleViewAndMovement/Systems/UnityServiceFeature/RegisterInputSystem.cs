using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems.UnityServiceFeature {
    public class RegisterInputSystem : IInitializeSystem {
        private readonly InputContext m_contexts;
        private readonly IInputServices m_inputServices;

        public RegisterInputSystem(Contexts contexts, IInputServices inputServices) {
            m_contexts = contexts.input;
            m_inputServices = inputServices;
        }

        public void Initialize() {
            m_contexts.ReplaceInputService(m_inputServices);
        }
    }
}