using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Serializable]
    public class Animations
    {
        public List<Sprite> sprites;
        [HideInInspector]
        public List<Sprite> renderSprites;
        public TextAsset Config;
        public float RenderSpeed;
    }

    public enum Stages
    {
        Stand,
        Move,
        Attack,
        Hit,
        Die
    }


    // Start is called before the first frame update
    [Header("Move")]
    public Animations sprites_Move;
    [Header("Stand")]
    public Animations sprites_Stand;
    [Header("Hit")]
    public Animations sprites_Hit;
    [Header("Die")]
    public Animations sprites_Die;
    [Header("Setting Pivot")]
    public bool floatRight = false;

    [Header("Setting Attribute")]
    public EnemyAttribute m_attribute;

    public float RadiusCircleColiider = 0.01f;
    public Stages m_stages;
    Stages m_PreStage;
    protected int indexFrame = 0;
    protected SpriteRenderer render;
    protected float m_fcurrentTime = 0;
    protected float m_fcurrentTimeDoing = 0;
    protected float m_fMaxTimeMove;
    protected float m_fMaxTimeStand;
    protected bool m_CanHit = true;
    XmlDocument Xmldoc;
    Rigidbody2D m_rigidBody;

    public LayerMask m_LayerGround;
    public bool m_isFacingRight;
    public EnemyProperties properties;

    Animator m_Anim;

    public virtual void Start()
    {
        //Resources.LoadAll("",  typeof(Sprite));
        render = GetComponent<SpriteRenderer>();
        m_fcurrentTime = 0;
        //Init pivot Move
        IAnimations.InitPivot(ref sprites_Move, floatRight);
        ////Init pivot Stand
        IAnimations.InitPivot(ref sprites_Stand, floatRight);
        ////Init pivot Hit
        IAnimations.InitPivot(ref sprites_Hit, floatRight);
        //Init Die
        IAnimations.InitPivot(ref sprites_Die, floatRight);

        InitCollider();
        m_rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
        m_rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Init Time move
        m_fMaxTimeMove = UnityEngine.Random.Range(2f, 4f);
        m_fMaxTimeStand = UnityEngine.Random.Range(1f, 3f);

        //Flip
        if (!m_isFacingRight)
        {
            render.flipX = true;
        }

        //Set event die
        m_attribute.OnDieTrigger += OnDieTrigger;

    }

    void Update()
    {
        Draw();
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject.GetComponent<CircleCollider2D>());
            Destroy(m_rigidBody);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlayerManager.instance.GetPlayerTag())
        {
            Logs.LogD("Collider with enemy");
            PlayerManager.instance.SetAttack(m_attribute);
        }
        if (other.tag == "Bullet")
        {
            switch (m_stages)
            {
                case Stages.Attack: m_PreStage = Stages.Attack; break;
                case Stages.Move: m_PreStage = Stages.Move; break;
                case Stages.Stand: m_PreStage = Stages.Stand; break;
            }
            if(m_CanHit)
            {
                m_stages = Stages.Hit;
                StartCoroutine(OnHit());
            }
            Attack();
            Destroy(other.gameObject);
        }
        if (other.tag == PlayerManager.instance.GetPlayerAttackTags())
        {
            switch(m_stages)
            {
                case Stages.Attack: m_PreStage = Stages.Attack;break;
                case Stages.Move: m_PreStage = Stages.Move;break;
                case Stages.Stand: m_PreStage = Stages.Stand;break;
            }
            if (m_CanHit)
            {
                m_stages = Stages.Hit;
                StartCoroutine(OnHit());
            }
            Attack();
        }
    }

    public IAttributes GetAttribute()
    {
        return m_attribute;
    }
    void Attack()
    {
        if (m_attribute != null)
        {
            if(PlayerManager.instance.GetPlayerAttributes() != null)
            {
                m_attribute.TakeDamage(PlayerManager.instance.GetPlayerAttributes());
            }
        }
        else
            Logs.LogW("Enemy attribute is null");
    }
    void OnDieTrigger()
    {
        StopAllCoroutines();
        StartCoroutine(OnDie());
        m_stages = Stages.Die;
    }
    private void InitCollider()
    {
        //var TriggerCollision = gameObject.AddComponent<BoxCollider2D>();
        //TriggerCollision.size = new Vector2(1, 2);
        //TriggerCollision.isTrigger = true;

        var boxCollider = this.gameObject.AddComponent<CircleCollider2D>();
        boxCollider.radius = RadiusCircleColiider;
        boxCollider.offset.Set(0, 0.2f);
    }

    public virtual void Draw()
    {
        switch (m_stages)
        {
            case Stages.Stand:m_CanHit = true; Render(sprites_Stand); OnStand(); break;
            case Stages.Move:m_CanHit = true; Render(sprites_Move); OnMove(); break;
        }
    }

    virtual public void Move()
    {
        if(m_stages == Stages.Move)
        {
            transform.Translate(Vector2.right * properties.m_Speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(properties.positionShootOrGround.position, Vector2.down, properties.m_Distance, m_LayerGround);
            if (groundInfo.collider == false)
            {
                if (m_isFacingRight == true)
                {
                    Flip(0);
                }
                else
                {
                    Flip(180);
                }
            }
        }
    }

    public IEnumerator RandomChangeMove()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
        if(UnityEngine.Random.Range(0,5) > 2)
        {
            if (m_isFacingRight == false)
            {
                Flip(180);
            }
            else
            {
                Flip(0);
            }
        }
        StartCoroutine(RandomChangeMove());
    }

    public void Flip(float Angle)
    {
        transform.eulerAngles = new Vector3(0, Angle, 0);
        m_isFacingRight = !m_isFacingRight;
    }

    public void Render(Animations anim)
    {
        if (indexFrame >= anim.renderSprites.Count)
            return;
        render.sprite = anim.renderSprites[indexFrame];
        if (m_fcurrentTime > anim.RenderSpeed)
        {
            indexFrame++;
            indexFrame = indexFrame % anim.renderSprites.Count;
            m_fcurrentTime = 0;
        }
        else
        {
            m_fcurrentTime += Time.deltaTime;
        }
    }
    protected void OnStand()
    {
        if(SceneLoad.instance.GetComponent<Animator>().GetBool("Loading"))
        {
            return;
        }
        if (m_fcurrentTimeDoing < m_fMaxTimeStand)
        {
            m_fcurrentTimeDoing += Time.deltaTime;
        }
        else
        {
            m_stages = Stages.Move;
            m_fcurrentTimeDoing = 0;
            indexFrame = 0;
        }
    }
    public virtual void OnMove()
    {
        if (m_fcurrentTimeDoing < m_fMaxTimeMove)
        {
            m_fcurrentTimeDoing += Time.deltaTime;
        }
        else
        {
            m_stages = Stages.Stand;
            m_fcurrentTimeDoing = 0;
            indexFrame = 0;
        }
    }

    IEnumerator OnHit()
    {
        GameObject GO = Player_Controller.instance.GetHitObject();
        if(GO != null)
        {
            GameObject eff = Instantiate(Player_Controller.instance.GetHitObject(), transform);
        }
        render.sprite = sprites_Hit.renderSprites[0];
        yield return new WaitForSeconds(0.5f);
        indexFrame = 0;
        m_stages = m_PreStage;
        //Next Stage
    }

    IEnumerator OnDie()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        indexFrame = 0;
        m_fcurrentTime = 0;
        while (indexFrame < sprites_Die.renderSprites.Count)
        {
            yield return null;
            render.sprite = sprites_Die.renderSprites[indexFrame];
            if (m_fcurrentTime > sprites_Die.RenderSpeed)
            {
                indexFrame++;
                if (indexFrame >= sprites_Die.renderSprites.Count - 1)
                    break;
                m_fcurrentTime = 0;
            }
            else
            {
                m_fcurrentTime += Time.deltaTime;
            }
        }
        CreateItem();
        Destroy(this.transform.parent.gameObject);
    }

    void CreateItem()
    {
        Logs.LogD("Create item");
        GameObject go = Resources.Load("Items/ItemPrefabs") as GameObject;
        if(go != null)
        {
            Equipment equipment = ItemManager.instance.GetItem(1001);
            //equipment.Name = "book";
            //equipment.Damage = 50;
            //equipment.Heath = 50;
            //equipment.Defense = 50;
            //equipment.quanlity = Quanlity.A;
            //equipment.icon = Resources.Load<Sprite>("Items/book");
            if (equipment == null)
                return;
            go.GetComponent<PickItem>().SetItem(equipment, equipment.icon);
            Vector3 offset = new Vector3(0, 0.05f,0);
            Instantiate(go, transform.position + offset, Quaternion.identity);
        }
    }
}
