using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace App.Scripts.ECS.HelloWorld {
    public class DebugMessageSystem : ReactiveSystem<GameEntity> {
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
            return context.CreateCollector(GameMatcher.AppScriptsECSHelloWorldDebugMessage);
        }

        protected override bool Filter(GameEntity entity) {
            return entity.hasAppScriptsECSHelloWorldDebugMessage;
        }

        protected override void Execute(List<GameEntity> entities) {
            foreach (var entity in entities) {
                Debug.Log(entity.appScriptsECSHelloWorldDebugMessage.Message);
            }
        }

        public DebugMessageSystem(Contexts contexts) : base(contexts.game) { }
    }
}