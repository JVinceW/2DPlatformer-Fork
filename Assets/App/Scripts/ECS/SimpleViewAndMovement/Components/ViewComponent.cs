using Entitas;
using UnityEngine;

[MoveTutGame]
// ReSharper disable once CheckNamespace
public class ViewComponent : IComponent {
    public GameObject ViewGameObject;
}

[MoveTutGame]
public class SpriteComponent : IComponent {
    public string SpriteName;
}