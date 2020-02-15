using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace App.Scripts.ECS.SimpleViewAndMovement.Components {
    [Game, MoveTutGame, Unique]
    public class CameraServiceComponent : IComponent{
        public ICameraService CameraService;
    }
}