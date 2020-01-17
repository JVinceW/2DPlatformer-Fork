using UnityEngine;

[System.Serializable]
public class EnemyProperties
{
    [Header("Attribute")]
    public float m_Distance;
    public float m_Speed;

    [Header("Transform")]
    private Transform transform;
    public Transform positionShootOrGround;
    public Transform positionHeath;
}
