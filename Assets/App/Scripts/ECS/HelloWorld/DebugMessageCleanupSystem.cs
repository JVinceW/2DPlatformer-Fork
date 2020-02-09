using Entitas;

namespace App.Scripts.ECS.HelloWorld {
    public class DebugMessageCleanupSystem  : ICleanupSystem {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly GameContext m_gameContext;
        private readonly IGroup<GameEntity> m_grp;

        public DebugMessageCleanupSystem(Contexts gameContext) {
            m_gameContext = gameContext.game;
            m_grp = m_gameContext.GetGroup(GameMatcher.AppScriptsECSHelloWorldDebugMessage);
        }

        public void Cleanup() {
            foreach (var e in m_grp.GetEntities()) {
                e.Destroy();
            }
        }
    }
}