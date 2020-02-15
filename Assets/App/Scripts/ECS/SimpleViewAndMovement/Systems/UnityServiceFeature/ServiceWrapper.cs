using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;

namespace App.Scripts.ECS.SimpleViewAndMovement.Systems.UnityServiceFeature {
    public class ServiceWrapper {
        public readonly IInputServices InputServices;
        public readonly ICameraService CameraService;

        public ServiceWrapper(IInputServices inputServices, ICameraService cameraService) {
            InputServices = inputServices;
            CameraService = cameraService;
        }
    }
}