using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy
{
    public EnemyProperties m_properties;

    public void Init()
    {
        Logs.LogD("Init Enemy - IEnemy");
    }

    public abstract void Update();
}
