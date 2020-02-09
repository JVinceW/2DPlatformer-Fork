using System.Collections.Generic;
using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class RenderPositionSystem : ReactiveSystem<MoveTutGameEntity> {
        private readonly MoveTutGameContext m_contexts;

        public RenderPositionSystem(Contexts context) : base(context.moveTutGame) {
            m_contexts = context.moveTutGame;
        }

        protected override ICollector<MoveTutGameEntity> GetTrigger(IContext<MoveTutGameEntity> context) {
            return context.CreateCollector(MoveTutGameMatcher.Position);
        }

        protected override bool Filter(MoveTutGameEntity entity) {
            return entity.hasView && entity.hasPosition;
        }

        protected override void Execute(List<MoveTutGameEntity> entities) {
            foreach (var e in entities) {
                var transform = e.view.ViewGameObject.transform;
                transform.position = e.position.Value;
            }
        }
    }
}