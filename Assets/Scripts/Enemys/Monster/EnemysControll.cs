using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemysControll : MonoBehaviour
{
    [System.Serializable]
    public struct Moving
    {
        public Transform m_GroundDetection;
        public float m_Distance;
        
        public float m_speed;
        
    }
    [System.Serializable]
    public struct Shooting
    {
        public GameObject bullet;
        public Transform PositionShoot;
        public float TimeToShoot;
        public int Dir;
    }
    public enum TypeEnemy { Shooting, Moving };

    [Header("Config Enemy")]
    public TypeEnemy m_TypeEnemy;
    //public Moving m_Moving;
    //public Shooting m_Shooting;
    //public Transform PosHeath;
    //EnemyMoving enemyMoving;
    //EnemyShooting enemyShooting;

    public LayerMask m_LayerGround;
    public bool m_isFacingRight;
    EnemyManager m_Enemy;
    public EnemyProperties properties;

    Animator m_Anim;
    bool m_isDead = false;
    

    void Start()
    {
        m_Anim = GetComponent<Animator>();
        if (m_TypeEnemy == TypeEnemy.Moving)
        {
            //m_Enemy = new EnemyManager(new EnemyMoving(properties,this.transform, m_LayerGround, m_isFacingRight));
        }
        if(m_TypeEnemy == TypeEnemy.Shooting)
        {
            //m_Enemy = new EnemyManager(new EnemyShooting(properties));
        }
        //m_Enemy.Init();
    }

    private void OnDrawGizmosSelected()
    {
    }

    void Update()
    {
        if(m_isDead == false)
        {
            //m_Enemy.Update();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlayerManager.instance.GetPlayerTag())
        {
            Logs.LogD("Player collider with Enemy");
            //PlayerManager.instance.SetAttack(m_Enemy.attribute.Damage);
            //m_playerManager.AddForce(transform.position);
        }
        if (other.tag == "Bullet")
        {
            Attack();
            Destroy(other.gameObject);
        }
    }
    private void OnGUI()
    {
        if (DebugManager.instance.isDebug())
        {
            Vector2 size = new Vector2(60, 20);
            Vector3 pos = Camera.main.WorldToScreenPoint(properties.positionHeath.position);
            pos.y = Screen.height - pos.y;
            //GUI.Box(new Rect(pos.x - size.x / 2, pos.y, size.x, size.y), m_Enemy.attribute.CurrentHeath + "/" + m_Enemy.attribute.Heath);
            GUI.enabled = true;

        }

    }

    void Attack()
    {
        //m_Enemy.Attack(PlayerManager.instance.GetPlayerDamage(), PlayerManager.instance.GetPlayerCrits());
        //if (m_Enemy.attribute.CurrentHeath <= 0)
        //{
        //    if(m_Anim != null)
        //    {
        //        m_isDead = true;
        //        m_Anim.SetBool("isDead", true);
        //    }
        //    else
        //    {
        //        KillObject();
        //    }
        //}
    }
    public void KillObject()
    {
        Destroy(transform.parent.gameObject);
        //if (properties.EffectDestroy != null)
        //{
        //    GameObject effectDestroy = Instantiate(properties.EffectDestroy, transform.position, Quaternion.identity);
        //    Destroy(effectDestroy, 1f);
        //}
    }
}
