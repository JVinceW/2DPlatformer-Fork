using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Interfaces {
    public interface ICameraService {
        Vector2 ScreenToWorldPoint(Vector3 worldPosition);
    }
}