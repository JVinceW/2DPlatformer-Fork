using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace App.Scripts.ECS.SimpleViewAndMovement.Components {
    [Input, Unique]
    public class InputServiceComponent : IComponent {
        public IInputServices InputServices;
    }
}