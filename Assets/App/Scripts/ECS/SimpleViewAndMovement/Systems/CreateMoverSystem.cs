using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class CreateMoverSystem : ReactiveSystem<InputEntity> {
        private readonly MoveTutGameContext m_context;
        public CreateMoverSystem(Contexts contexts) : base(contexts.input) {
            m_context = contexts.moveTutGame;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
            return context.CreateCollector(InputMatcher.AllOf(InputMatcher.RightMouse, InputMatcher.MouseDown));
        }

        protected override bool Filter(InputEntity entity) {
            return entity.hasMouseDown;
        }

        protected override void Execute(List<InputEntity> entities) {
            foreach (var e in entities) {
                var mover = m_context.CreateEntity();
                mover.isMover = true;
                mover.AddPosition(e.mouseDown.Position);
                mover.AddDirection(Random.Range(0, 360));
                mover.AddSprite("Enemys/Monsers/MushRoom_leaf/hit1/0");
            }
        }
    }
}