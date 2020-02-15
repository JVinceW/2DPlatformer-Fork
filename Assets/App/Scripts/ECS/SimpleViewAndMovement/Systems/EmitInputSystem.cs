using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class EmitInputSystem : IInitializeSystem, IExecuteSystem {
        private readonly InputContext m_inputContext;
        private readonly Contexts m_contexts;

        private InputEntity m_leftInputEntity;
        private InputEntity m_rightInputEntity;

        public EmitInputSystem(Contexts contexts) {
            m_contexts = contexts;
            m_inputContext = contexts.input;
        }

        public void Initialize() {
            m_inputContext.isLeftMouse = true;
            m_leftInputEntity = m_inputContext.leftMouseEntity;

            m_inputContext.isRightMouse = true;
            m_rightInputEntity = m_inputContext.rightMouseEntity;
        }

        public void Execute() {
            var cameraService = m_contexts.moveTutGame.cameraService.CameraService;
            var inputService = m_contexts.input.inputService.InputServices;
            var mousePos = cameraService.ScreenToWorldPoint(inputService.MousePosition);
            if (inputService.GetMouseButtonDown(0)) {
                m_leftInputEntity.ReplaceMouseDown(mousePos);
            }

            if (inputService.GetMouseButton(0)) {
                m_leftInputEntity.ReplaceMousePosition(mousePos);
            }

            if (inputService.GetMouseButtonUp(0)) {
                m_leftInputEntity.ReplaceMouseUp(mousePos);
            }

            if (inputService.GetMouseButtonDown(1)) {
                m_rightInputEntity.ReplaceMouseDown(mousePos);
            }

            if (inputService.GetMouseButton(1)) {
                m_rightInputEntity.ReplaceMousePosition(mousePos);
            }

            if (inputService.GetMouseButtonUp(1)) {
                m_rightInputEntity.ReplaceMouseUp(mousePos);
            }
        }
    }
}