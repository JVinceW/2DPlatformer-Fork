using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

// ReSharper disable once CheckNamespace
public interface IDestroyableEntity : IEntity, IDestroyedEntity, IViewEntity { }

public partial class MoveTutGameEntity : IDestroyableEntity { }

public partial class InputEntity : IDestroyableEntity { }

public class MultiDestroySystem : MultiReactiveSystem<IDestroyableEntity, Contexts> {
    public MultiDestroySystem(Contexts contexts) : base(contexts) { }

    protected override ICollector[] GetTrigger(Contexts contexts) {
        return new ICollector[] {
            contexts.moveTutGame.CreateCollector(MoveTutGameMatcher.Destroyed),
            contexts.input.CreateCollector(InputMatcher.Destroyed)
        };
    }

    protected override bool Filter(IDestroyableEntity entity) {
        return entity.isDestroyed;
    }

    protected override void Execute(List<IDestroyableEntity> entities) {
        foreach (var e in entities) {
            if (e.hasView) {
                var go = e.view.GameObject;
                go.Unlink();
                go.DestroyGameObject();
            }
            Debug.Log("Destroyed Entity from " + e.contextInfo.name + " context");
            e.Destroy();
        }
    }
}