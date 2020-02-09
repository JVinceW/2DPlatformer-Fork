using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class AddViewSystem : ReactiveSystem<MoveTutGameEntity> {
        private readonly MoveTutGameContext m_contexts;
        private readonly Transform m_view = new GameObject("Game Views").transform;

        public AddViewSystem(Contexts contexts) : base(contexts.moveTutGame) {
            m_contexts = contexts.moveTutGame;
        }

        protected override ICollector<MoveTutGameEntity> GetTrigger(IContext<MoveTutGameEntity> context) {
            return context.CreateCollector(MoveTutGameMatcher.Sprite);
        }

        protected override bool Filter(MoveTutGameEntity entity) {
            return entity.hasSprite && !entity.hasView;
        }

        protected override void Execute(List<MoveTutGameEntity> entities) {
            foreach (var entity in entities) {
                GameObject go = new GameObject("Game View");
                go.transform.SetParent(m_view, false);
                entity.AddView(go);
                go.Link(entity);
            }
        }
    }
}