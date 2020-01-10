using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseEnemy
{
    override public void Start()
    {
        base.Start();
        //Random flip move
        StartCoroutine(RandomChangeMove());
    }
    public override void Draw()
    {
        switch (m_stages)
        {
            case Stages.Stand: Render(sprites_Stand); OnStand(); break;
            case Stages.Move: Render(sprites_Move); OnMove(); break;
        }
    }
}
