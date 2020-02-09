using Entitas;

namespace App.Scripts.ECS {
    public class DebugMessageCleanupSystem  : ICleanupSystem {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly GameContext m_gameContext;
        private readonly IGroup<GameEntity> m_grp;

        public DebugMessageCleanupSystem(Contexts gameContext) {
            m_gameContext = gameContext.game;
            m_grp = m_gameContext.GetGroup(GameMatcher.AppScriptsECSDebugMessage);
        }

        public void Cleanup() {
            foreach (var e in m_grp.GetEntities()) {
                e.Destroy();
            }
        }
    }
}