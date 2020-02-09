using Entitas;
using UnityEngine;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem {
    private readonly InputContext m_context;
    private InputEntity m_leftInputEntity;
    private InputEntity m_rightInputEntity;
        

    public EmitInputSystem(Contexts contexts) {
        m_context = contexts.input;
    }
    
    public void Initialize() {
        m_context.isLeftMouse = true;
        m_leftInputEntity = m_context.leftMouseEntity;

        m_context.isRightMouse = true;
        m_rightInputEntity = m_context.rightMouseEntity;
    }

    public void Execute() {
        if (Camera.main != null) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0)) {
                m_leftInputEntity.ReplaceMouseDown(mousePos);
            }

            if (Input.GetMouseButton(0)) {
                m_leftInputEntity.ReplaceMousePosition(mousePos);
            }

            if (Input.GetMouseButtonUp(0)) {
                m_leftInputEntity.ReplaceMouseUp(mousePos);
            }

            if (Input.GetMouseButtonDown(1)) {
                m_rightInputEntity.ReplaceMouseDown(mousePos);
            }

            if (Input.GetMouseButton(1)) {
                m_rightInputEntity.ReplaceMousePosition(mousePos);
            }

            if (Input.GetMouseButtonUp(1)) {
                m_rightInputEntity.ReplaceMouseUp(mousePos);
            }
        }
    }
}