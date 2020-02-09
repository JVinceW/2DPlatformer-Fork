using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class MovementSystemFeature : Feature {
        public MovementSystemFeature(Contexts contexts) : base("Movement System") {
            Add(new MoveSystem(contexts));
        }

        public sealed override Entitas.Systems Add(ISystem system) {
            return base.Add(system);
        }
    }
}