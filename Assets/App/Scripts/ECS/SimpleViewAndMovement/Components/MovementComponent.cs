using Entitas;
using UnityEngine;

[MoveTutGame]
// ReSharper disable once CheckNamespace
public class MoverComponent : IComponent { }

[MoveTutGame]
public class MoveComponent : IComponent {
    public Vector2 Target;
}

[MoveTutGame]
public class MoveCompleteComponent : IComponent { }