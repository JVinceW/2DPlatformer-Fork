using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Interfaces {
    public interface IInputServices {
        bool GetMouseButtonDown(int btnIdx);
        bool GetMouseButton(int btnIdx);
        bool GetMouseButtonUp(int btnIdx);

        Vector2 MousePosition { get; }
    }
}