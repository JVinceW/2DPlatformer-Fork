using Entitas;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems {
    public class ViewSystemFeature : Feature {
        public ViewSystemFeature(Contexts contexts) : base("View System") {
            Add(new AddViewSystem(contexts));
            Add(new RenderDirectionSystem(contexts));
            Add(new RenderPositionSystem(contexts));
            Add(new RenderSpriteSystem(contexts));
        }

        public sealed override Entitas.Systems Add(ISystem system) {
            return base.Add(system);
        }
    }
}