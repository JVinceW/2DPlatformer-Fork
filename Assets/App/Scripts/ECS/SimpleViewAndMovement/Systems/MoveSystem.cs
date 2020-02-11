using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class MoveSystem : IExecuteSystem, ICleanupSystem {
    private readonly IGroup<MoveTutGameEntity> m_move;
    private readonly IGroup<MoveTutGameEntity> m_moveComplete;
    private const float SPEED = 4f;

    public MoveSystem(Contexts contexts) {
        m_move = contexts.moveTutGame.GetGroup(MoveTutGameMatcher.Move);
        m_moveComplete = contexts.moveTutGame.GetGroup(MoveTutGameMatcher.MoveComplete);
    }
    
    public void Execute() {
        foreach (var e in m_move.GetEntities()) {
            Vector2 dir = e.move.Target - e.position.Value;
            Vector2 nowPos = e.position.Value + dir.normalized * (SPEED * Time.deltaTime);
            e.ReplacePosition(nowPos);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            e.ReplaceDirection(angle);

            float dist = dir.magnitude;
            if (dist <= 0.5f) {
                e.RemoveMove();
                e.isMoveComplete = true;
            }
        }
    }

    public void Cleanup() {
        foreach (var e in m_moveComplete.GetEntities()) {
            e.isMoveComplete = false;
            e.isDestroyed = true;
        }
    }
}