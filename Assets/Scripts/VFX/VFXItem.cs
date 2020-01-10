using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class VFXItem : MonoBehaviour
{
    public ParticleSystem m_particleSystem;
    public Gradient mColor;

    private void Start()
    {
        if (m_particleSystem == null)
            return;
        var col = m_particleSystem.colorOverLifetime;
        col.enabled = true;
        col.color = mColor;
    }
}
