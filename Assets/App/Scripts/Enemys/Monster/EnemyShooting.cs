using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting:IEnemy
{
    float ShootTime = 0;
    bool isAttack = true;

    public EnemyShooting(EnemyProperties properties)
    {
        //bullet = sbullet;
        //posSpawm = sPos;
        //this.transform = transform;
        //this.TimeShoot = TimeShoot;
        //dir = Dir;
        m_properties = properties;
    }
    public override void Update()
    {
        if(isAttack)
        {
            if(ShootTime >= m_properties.m_Speed)
            {
                //Inst bullet
                //GameObject bull = GameObject.Instantiate(m_properties.BulletgameObject, m_properties.positionShootOrGround.position, m_properties.positionShootOrGround.rotation) as GameObject;
                //bull.GetComponent<bulletControll>().SetDame(attribute.Damage);
                ShootTime = 0;
            }
            else
            {
                ShootTime += Time.deltaTime;
            }
        }
    }
}
