using Entitas;

namespace App.Scripts.ECS {
    public class HelloWorldSystem : IInitializeSystem {
        private GameContext m_context;

        public HelloWorldSystem(Contexts context) {
            m_context = context.game;
        }

        public void Initialize() {
            m_context.CreateEntity().AddAppScriptsECSDebugMessage("Hello World");
        }
    }
}