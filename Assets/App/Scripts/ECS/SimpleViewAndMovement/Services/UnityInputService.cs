using App.Scripts.ECS.SimpleViewAndMovement.Interfaces;
using UnityEngine;

namespace App.Scripts.ECS.SimpleViewAndMovement.Services {
    public class UnityInputService : IInputServices {
        public bool GetMouseButtonDown(int btnIdx) {
            return Input.GetMouseButtonDown(btnIdx);
        }

        public bool GetMouseButton(int btnIdx) {
            return Input.GetMouseButton(btnIdx);
        }

        public bool GetMouseButtonUp(int btnIdx) {
            return Input.GetMouseButtonUp(btnIdx);
        }

        public Vector2 MousePosition => Input.mousePosition;
    }
}