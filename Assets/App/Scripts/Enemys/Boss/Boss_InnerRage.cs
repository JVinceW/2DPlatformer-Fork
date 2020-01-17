using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_InnerRage : BaseEnemy
{
    [Header("Skill_1")]
    public Animations sprites_Skill_1;
    [Header("Skill_2")]
    public Animations sprites_Skill_2;
    Animations m_CurrentSkill;

    public List<Transform> m_LstMove;
    int moveIndex = 0;
    public Slider m_Heathbar;
    public BoxCollider2D m_AttackCollider;

    Vector2 target_Move;

    public override void Start()
    {
        base.Start();
        IAnimations.InitPivot(ref sprites_Skill_1, floatRight);
        IAnimations.InitPivot(ref sprites_Skill_2, floatRight);
        m_CurrentSkill = sprites_Skill_1;
        target_Move = Vector2.zero;
        target_Move = new Vector2(Random.Range((int)m_LstMove[0].position.x, (int)m_LstMove[1].position.x), m_LstMove[0].position.y);
        m_stages = Stages.Stand;
    }

    private void LateUpdate()
    {
        UpdateHeathBar();
    }

    public override void Draw()
    {
        switch (m_stages)
        {
            case Stages.Stand: m_CanHit = true; Render(sprites_Stand); OnStand(); break;
            case Stages.Move: m_CanHit = true; Render(sprites_Move); OnMove(); break;
            case Stages.Attack: m_CanHit = false; OnAttack(m_CurrentSkill);break;
        }
    }

    public override void Move()
    {
        if (m_stages == Stages.Move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target_Move, properties.m_Speed);
            if (Vector2.Distance(transform.position, target_Move) < 0.2f)
            {
                moveIndex++;
                moveIndex = moveIndex % m_LstMove.Count;
                Vector2 moveTaget = new Vector2(PlayerManager.instance.GetPostition().x, target_Move.y);
                target_Move = moveTaget;
                m_stages = Stages.Attack;
                m_AttackCollider.enabled = true;

                if (transform.position.x > PlayerManager.instance.GetPostition().x)
                {
                    Flip(180);
                }
                else
                {
                    Flip(0);
                }

            }
        }
    }
    public override void OnMove()
    {
        
    }
    void OnAttack(Animations anim)
    {
        if (indexFrame >= anim.renderSprites.Count)
            return;
        render.sprite = anim.renderSprites[indexFrame];
        DrawCollision(anim.renderSprites[indexFrame]);
        if (m_fcurrentTime > anim.RenderSpeed)
        {
            indexFrame++;
            if(indexFrame >= anim.renderSprites.Count)
            {
                m_stages = Stages.Stand;
                if(m_CurrentSkill == sprites_Skill_1)
                {
                    m_CurrentSkill = sprites_Skill_2;
                }
                else
                {
                    m_CurrentSkill = sprites_Skill_1;
                }

                m_AttackCollider.enabled = false;

            }
            indexFrame = indexFrame % anim.renderSprites.Count;
            m_fcurrentTime = 0;
        }
        else
        {
            m_fcurrentTime += Time.deltaTime;
        }
    }

    void DrawCollision(Sprite sprite)
    {
        float width = sprite.texture.width;
        float height = sprite.texture.height;
        if( width < 1000)
        {
            while (width > 10)
            {
                width = width / 10;
            }
        }
        else
        {
            width /= 100;
        }
        if( height < 1000)
        {
            while (height > 10)
            {
                height = height / 10;
            }
        }
        else
        {
            height /= 100;
        }
        if (m_AttackCollider == null)
            return;
        m_AttackCollider.size = new Vector2(width, height);
        m_AttackCollider.offset = new Vector2(0, height / 2);
    }

    void UpdateHeathBar()
    {
        if(m_Heathbar == null)
        {
            Logs.LogD("Heathbar of Boss " + this.gameObject.name + " is null");
            return;
        }
        m_Heathbar.value = (float)m_attribute.GetCurrentHeath() / (float)m_attribute.Heath.GetValue();
    }
}
