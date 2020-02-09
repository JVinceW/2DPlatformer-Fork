using Entitas;

// ReSharper disable once CheckNamespace
public class InputSystemFeature : Feature {
    public InputSystemFeature(Contexts contexts) : base("Input System") {
        Add(new MoveCommandSystem(contexts));
        Add(new CreateMoverSystem(contexts));
        Add(new EmitInputSystem(contexts));
    }

    public sealed override Systems Add(ISystem system) {
        return base.Add(system);
    }
}