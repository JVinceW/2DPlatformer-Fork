using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace App.Scripts.ECS {
    public class DebugMessageSystem : ReactiveSystem<GameEntity> {
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
            return context.CreateCollector(GameMatcher.AppScriptsECSDebugMessage);
        }

        protected override bool Filter(GameEntity entity) {
            return entity.hasAppScriptsECSDebugMessage;
        }

        protected override void Execute(List<GameEntity> entities) {
            foreach (var entity in entities) {
                Debug.Log(entity.appScriptsECSDebugMessage.Message);
            }
        }

        public DebugMessageSystem(Contexts contexts) : base(contexts.game) { }
    }
}