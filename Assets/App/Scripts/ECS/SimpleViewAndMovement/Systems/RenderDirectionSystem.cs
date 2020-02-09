using System.Collections.Generic;
using Entitas;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class RenderDirectionSystem : ReactiveSystem<MoveTutGameEntity> {
    private readonly MoveTutGameContext m_context;
    public RenderDirectionSystem(Contexts contexts) : base(contexts.moveTutGame) {
        m_context = contexts.moveTutGame;
    }

    protected override ICollector<MoveTutGameEntity> GetTrigger(IContext<MoveTutGameEntity> context) {
        return context.CreateCollector(MoveTutGameMatcher.Direction);
    }

    protected override bool Filter(MoveTutGameEntity entity) {
        return entity.hasView && entity.hasDirection;
    }

    protected override void Execute(List<MoveTutGameEntity> entities) {
        foreach (var e in entities) {
            var trans = e.view.ViewGameObject.transform;
            var angle = e.direction.Value;
            trans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}