namespace App.Scripts.ECS.SimpleViewAndMovement.Systems.UnityServiceFeature {
    public sealed class RegisterUnityServiceFeature : Feature {
        public RegisterUnityServiceFeature(Contexts contexts, ServiceWrapper serviceWrapper) {
            Add(new RegisterInputSystem(contexts, serviceWrapper.InputServices));
            Add(new RegisterUnityCameraSystem(contexts, serviceWrapper.CameraService));
        }
    }
}