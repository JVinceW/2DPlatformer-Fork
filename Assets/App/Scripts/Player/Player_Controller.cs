using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    static public Player_Controller instance;
    public bool FacingRight;
    // Start is called before the first frame update
    [Header("Config Speed")]
    public bool m_facingRight;
    public float m_Speed;
    [Header("Config Jump")]
    public Transform m_WeaponPos;
    public float m_JumpForce;
    public Transform m_FeetPostion;
    public float m_checkRadius;
    public float m_JumpTimeCounter;
    public LayerMask m_LayerisGround;
    [Header("Dust")]
    public GameObject m_Dust;
    [Header("Hit Effect")]
    public GameObject m_ShootSkill;
    public GameObject m_HitEffect;
    /// 

    private Rigidbody2D m_body;
    private Animator m_anim;
    private GameObject CurrentWeapon;

    private string CurrentWeaponName;

    private float m_ScaleX, m_FlipScaleX;
    private float m_fJumpTimeCounter;
    //private float DustDelay = 0.1f;

    public bool m_isGround = false;
    private bool m_isJumped = false;
    private bool m_isAttacking = false;
    public bool m_CanMove = false;

    //Keep Player Inside Camera
    Vector2 m_ScreenBounds;
    Vector2 m_PlayerBounds;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(this.transform.parent.gameObject.tag);

        if (objs.Length > 1)
        {
            Destroy(this.transform.parent.gameObject);
            return;
        }

        DontDestroyOnLoad(this.transform.parent.gameObject);
        instance = this;
    }

    void Start()
    {
        //Get Screen And PLayer Bound
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        m_PlayerBounds.x = transform.GetComponent<SpriteRenderer>().bounds.size.x + 2.5f;
        m_PlayerBounds.y = transform.GetComponent<SpriteRenderer>().bounds.size.y + 2.5f;

        m_body = GetComponentInParent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_ScaleX = GetComponent<Transform>().transform.localScale.x;
        m_FlipScaleX = m_ScaleX*-1;
        //Die event
        PlayerAttribute.instance.OnDieTrigger += OnDie;
        //Set pos on new map
        SceneLoad.instance.onMapLoadedCallBack += OnMapLoaded;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_CanMove)
            return;
        if (m_isGround)
            m_isJumped = false;
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (m_isGround)
            {
                SetAnim("Stand", true);
                SetAnim("Walk", false);
                if (!m_isJumped)
                {
                    SetAnim("Jump", false);
                }
            }
            else
            {
                SetAnim("Stand", false);
            }
        }
        Jump();
        Move();
        Dash();
        ////ChangeWeapons
        //{
        //    if(Input.GetKeyDown(PlayerManager.instance.NextWeapon))
        //    {
        //        if(CurrentWeapon != null)
        //        {
        //            WeaponsManager.instance.Next();
        //            InstanWeapon();
        //        }
        //    }
        //    if (Input.GetKeyDown(PlayerManager.instance.PreWeapon))
        //    {
        //        if (CurrentWeapon != null)
        //        {
        //            WeaponsManager.instance.Previous();
        //            InstanWeapon();
        //        }
        //    }
        //}

    }
    private void Update()
    {
        
        //if(WeaponsManager.instance.CountWeapons() > 0)
        //{
        //    if(CurrentWeapon == null)
        //    {
        //        InstanWeapon();
        //    }
        //}
        //Skill
        DoSkill();
    }

    private void LateUpdate()
    {
        //Vector3 viewPos = transform.position;
        //viewPos.x = Mathf.Clamp(viewPos.x, m_ScreenBounds.x * -1 - m_PlayerBounds.x, m_ScreenBounds.x + m_PlayerBounds.x - m_PlayerBounds.x/5);
        ////viewPos.y = Mathf.Clamp(viewPos.y, m_ScreenBounds.y * -1 - m_PlayerBounds.y, m_ScreenBounds.y + m_PlayerBounds.y);
        //transform.position = viewPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Logs.LogD(collision.gameObject.layer + " = " + collision.gameObject.tag + "&& ground = " + m_isGround);
        if(collision.gameObject.tag == "Ground" && m_isGround == false)
        {
            //m_CanMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(collision.transform.parent.GetComponent<Boss_InnerRage>() != null)
            {
                PlayerManager.instance.SetAttack(collision.transform.parent.GetComponent<Boss_InnerRage>().GetAttribute());
            }
        }
    }

    private void OnDestroy()
    {
        Destroy(CurrentWeapon);
        CurrentWeaponName = "";
    }

    void Move()
    {
        // //Move And Jump
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            m_body.velocity = new Vector2(moveHorizontal * m_Speed, m_body.velocity.y);
            //float moveVertical = Input.GetAxisRaw("Vertical");
            if (m_isAttacking)
                return;
            if (moveHorizontal != 0)
            {
                if (m_isGround)
                {
                    SetAnim("Walk", true);
                    ResetAttack();
                    if (!m_isJumped)
                    {
                        SetAnim("Jump", false);
                    }
                    //if (DustDelay <= 0)
                    //{
                    //    SpawmDust();
                    //    DustDelay = 0.1f;
                    //}
                    //DustDelay -= 0.02f;
                }
                else
                {
                    SetAnim("Walk", false);
                }
            }
            //m_target.y = this.transform.position.y;

            if (moveHorizontal > 0 && !m_facingRight)//RIGHT
            {
                Flip();
            }
            if (moveHorizontal < 0 && m_facingRight)//LEFT
            {
                Flip();
            }
        }
        FacingRight = GetComponent<Transform>().transform.localScale.x == 1 ? true : false;
    }

    void Jump()
    {
        //Debug.DrawLine(m_FeetPostion.position, new Vector3(m_FeetPostion.position.x, m_FeetPostion.position.y - 0.05f, m_FeetPostion.position.z), Color.yellow);
        m_isGround = Physics2D.OverlapCircle(m_FeetPostion.position, m_checkRadius);
        //if(m_isGround)
        //{
        //    m_CanMove = true;
        //}
        //m_isGround = Physics2D.OverlapCircle(m_FeetPostion.position, m_checkRadius, m_LayerisGround);

        if (Input.GetButtonDown("Jump") && m_isGround)
        {
            m_isGround = false;
            SetAnim("Jump", true);
            m_isJumped = true;
            m_fJumpTimeCounter = m_JumpTimeCounter;
            SpawmDust();
            m_body.velocity = Vector2.up * m_JumpForce;
        }
        if (Input.GetKey(PlayerManager.instance.Jump) && m_isJumped)
        {
            if (m_fJumpTimeCounter > 0)
            {
                m_fJumpTimeCounter -= Time.deltaTime;
                m_body.velocity = Vector2.up * m_JumpForce;
            }
            else
            {
                m_isJumped = false;
            }
        }
        if (Input.GetKeyUp(PlayerManager.instance.Jump))
        {
            m_isJumped = false;
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(PlayerManager.instance.kDash))
        {
            //m_body.AddForce(Vector2.right * 5000);
            if (m_facingRight)
            {
                m_body.velocity = new Vector2(50f, m_body.velocity.y);
            }
            else
            {
                m_body.velocity = new Vector2(-50f, m_body.velocity.y);
            }
        }
    }

    public void OnMapLoaded(Vector3 vec)
    {
        this.transform.parent.transform.position = vec;
        m_CanMove = true;
    }
    private void InstanWeapon()
    {
        if (CurrentWeaponName == WeaponsManager.instance.GetWeaponName())
            return;
   
        if (CurrentWeapon != null)
            Destroy(CurrentWeapon);

        Logs.LogD("InstanWeapon");
        CurrentWeapon = Instantiate(WeaponsManager.instance.GetWeapon(), m_WeaponPos.position, Quaternion.identity);
        CurrentWeaponName = WeaponsManager.instance.GetWeaponName();
    }

    void SetAnim(string name, bool isDo)
    {
        m_anim.SetBool(name, isDo);
    }
    bool GetAnimBool(string name)
    {
        return m_anim.GetBool(name);
    }
    private void SpawmDust()
    {
        Instantiate(m_Dust, m_FeetPostion.position, Quaternion.identity);
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    void DoSkill()
    {
        if(m_isGround)
        {
            if(!m_isAttacking)
            {
                if(Input.GetKeyDown(PlayerManager.instance.skill3))
                {
                    if (GetAnimBool("Shoot"))
                    {
                        return;
                    }
                    SetAnim("Shoot", true);
                }
                if (Input.GetKeyDown(PlayerManager.instance.skill1))
                {
                    if (GetAnimBool("Attack_1") || GetAnimBool("Attack_2"))
                    {
                        return;
                    }
                    SetAnim("Walk", false);
                    m_isAttacking = true;
                    if(Random.Range(0,2) == 0)
                        SetAnim("Attack_1", true);
                    else
                        SetAnim("Attack_2", true);
                    return;
                }
                //if (Input.GetKeyDown(PlayerManager.instance.skill2))
                //{
                //    if(GetAnimBool("Attack_2") || GetAnimBool("Attack_1"))
                //    {
                //        return;
                //    }
                //    SetAnim("Walk", false);
                //    m_isAttacking = true;
                //    SetAnim("Attack_2", true);
                //    SetAnim("Attack_1", false);
                //    return;
                //}
                if (Input.GetKeyDown(PlayerManager.instance.skill2))
                {
                    if (GetAnimBool("Attack_2") || GetAnimBool("Attack_1"))
                    {
                        return;
                    }
                    SetAnim("Walk", false);
                    m_isAttacking = true;
                    SetAnim("Skill1", true);
                    SetAnim("Attack_2", false);
                    SetAnim("Attack_1", false);
                    return;
                }
                //if (Input.GetKeyDown(KeyCode.B))
                //{
                //    if (GetAnimBool("Attack_2") || GetAnimBool("Attack_1"))
                //    {
                //        return;
                //    }
                //    SetAnim("Walk", false);
                //    m_isAttacking = true;
                //    SetAnim("Attack_2", true);
                //    SetAnim("Attack_1", false);
                //    return;
                //}
            }
        }
    }

    public void CreateEffect()
    {
        if(m_ShootSkill == null)
        {
            Logs.LogD("Can not do skil, effect is null");
            return;
        }
        GameObject GO = Instantiate(m_ShootSkill, transform.position, Quaternion.Inverse(transform.rotation));
        if(m_facingRight)
            GO.transform.position = new Vector3(transform.position.x + 2f, transform.position.y+0.3f, 0);
        else
            GO.transform.position = new Vector3(transform.position.x - 2f, transform.position.y+0.3f, 0);
        //Logs.LogD("Position X=" + GO.transform.position.x + " Y=" + GO.transform.position.y);
    }
    public void ResetAttack()
    {
        if(GetAnimBool("Shoot"))
        {
            SetAnim("Shoot", false);
            return;
        }
        m_isAttacking = false;
        SetAnim("Attack_1", false);
        SetAnim("Attack_2", false);
        SetAnim("Skill1", false);
    }

    public GameObject GetHitObject()
    {
        if (m_HitEffect == null)
        {
            Logs.LogE("Hit Effect is null");
            return null;
        }
        return m_HitEffect;
    }
    void OnDie()
    {
        m_anim.SetTrigger("Die");
    }
    void DestroyObject()
    {
        Destroy(transform.parent.transform.gameObject);
    }
}
