using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class MoveCommandSystem : ReactiveSystem<InputEntity> {
        private readonly MoveTutGameContext m_gameContext;
        private IGroup<MoveTutGameEntity> m_movers;
        public MoveCommandSystem(Contexts contexts) : base(contexts.input) {
            m_gameContext = contexts.moveTutGame;
            m_movers = m_gameContext.GetGroup(MoveTutGameMatcher.AllOf(MoveTutGameMatcher.Mover)
                .NoneOf(MoveTutGameMatcher.Move));
        }
        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
            return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MouseDown));
        }

        protected override bool Filter(InputEntity entity) {
            return entity.hasMouseDown;
        }

        protected override void Execute(List<InputEntity> entities) {
            foreach (var e in entities) {
                var movers = m_movers.GetEntities();
                if (movers.Length <= 0) {
                    return;
                }
                movers[Random.Range(0, movers.Length)].ReplaceMove(e.mouseDown.Position);
            }
        }
    }
}