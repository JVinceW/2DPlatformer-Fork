using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootPosition:IEnemy
{
    float ShootTime = 0;
    bool isAttack = true;

    public EnemyShootPosition(EnemyProperties properties)
    {
        //bullet = sbullet;
        //posSpawm = sPos;
        //this.transform = transform;
        //this.TimeShoot = TimeShoot;
        //dir = Dir;
        //Damage = damage;
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

                float caculatorForce = Vector2.Distance(this.m_properties.positionShootOrGround.position, PlayerManager.instance.GetPostition()) * 30f;
                Logs.LogW("Forces = " + caculatorForce);

                //bull.GetComponent<Rigidbody2D>().AddForce(m_properties.positionShootOrGround.rotation * Vector2.one * caculatorForce);
                ShootTime = 0;
            }
            else
            {
                ShootTime += Time.deltaTime;
            }
        }
    }
}
