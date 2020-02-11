using Entitas;
using UnityEngine;

[MoveTutGame, Input]
// ReSharper disable once CheckNamespace
public class ViewComponent : IComponent {
    public GameObject GameObject;
}

[MoveTutGame]
public class SpriteComponent : IComponent {
    public string SpriteName;
}