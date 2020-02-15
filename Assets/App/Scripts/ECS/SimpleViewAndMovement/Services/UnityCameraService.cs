using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Services {
    public class UnityCameraService : ICameraService {
        public Vector2 ScreenToWorldPoint(Vector3 worldPosition) {
            if (Camera.main != null) 
                return Camera.main.ScreenToWorldPoint(worldPosition);
            
            return Vector2.zero;
        }
    }
}