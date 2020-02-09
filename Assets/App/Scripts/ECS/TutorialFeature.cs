using Entitas;

namespace App.Scripts.ECS {
    public class TutorialFeature : Feature {
        public TutorialFeature(Contexts contexts) : base("Tut System") {
            Add(new DebugMessageSystem(contexts));
            Add(new HelloWorldSystem(contexts));
            Add(new DebugMessageCleanupSystem(contexts));
        }

        public sealed override Entitas.Systems Add(ISystem system) {
            return base.Add(system);
        }
    }
}