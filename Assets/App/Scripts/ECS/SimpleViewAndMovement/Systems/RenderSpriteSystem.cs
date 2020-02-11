using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class RenderSpriteSystem : ReactiveSystem<MoveTutGameEntity> {
        public RenderSpriteSystem(Contexts contexts) : base(contexts.moveTutGame) { }

        protected override ICollector<MoveTutGameEntity> GetTrigger(IContext<MoveTutGameEntity> context) {
            return context.CreateCollector(MoveTutGameMatcher.Sprite);
        }

        protected override bool Filter(MoveTutGameEntity entity) {
            return entity.hasSprite && entity.hasView;
        }

        protected override void Execute(List<MoveTutGameEntity> entities) {
            foreach (var entity in entities) {
                GameObject go = entity.view.GameObject;
                var renderer = go.GetComponent<SpriteRenderer>();
                if (renderer == null) {
                    renderer = go.AddComponent<SpriteRenderer>();
                }

                renderer.sprite = Resources.Load<Sprite>(entity.sprite.SpriteName);
            }
        }
    }
}