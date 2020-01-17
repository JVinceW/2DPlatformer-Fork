using System.Collections.Generic;
using UnityEngine;

public class IBoss : MonoBehaviour
{
    [Header("Position Move")]
    public List<Transform> m_Points;
    int index = 0;

    [Header("Configs")]
    public float m_speed;
    public float timeDelay;
    [Header("Config Attribute")]
    public EnemyAttribute m_attribute;
    [Header("Skill Config")]
    public List<string> m_skills;
    public int SkillIndex = 0;
    public string m_strMove;

    Animator m_anim;
    float m_distanceMove;
    bool isDoing = false;
    StagesBoss m_stage = StagesBoss.idle;

    public enum StagesBoss
    {
        idle,
        move,
        skill
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        m_anim = GetComponent<Animator>();
        m_distanceMove = Vector2.Distance(transform.position, m_Points[index].position);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isDoing)
            return;
        switch(m_stage)
        {
            case StagesBoss.idle: idle(); break;
            case StagesBoss.move: Move(); break;
            case StagesBoss.skill: break;
        }
    }

    public virtual void idle()
    {
        isDoing = true;
        float elapsed = 0f;
        while(elapsed < timeDelay)
        {
            elapsed += Time.deltaTime;
        }
        m_stage = StagesBoss.skill;
        isDoing = false;
    }

    public virtual void Move()
    {
        if (CheckParemeterInAnimator(m_strMove))
        {
            Logs.LogW("Can't do move animation.Don't have parameter " + m_strMove + " in Animator");
        }
        else
        {
            m_anim.SetBool(m_strMove, true);
        }
        transform.position = Vector2.MoveTowards(transform.position, m_Points[index].position, m_speed * Time.deltaTime * m_distanceMove);
        if(Vector2.Distance(transform.position, m_Points[index].position) < 0.1f)
        {
            if (CheckParemeterInAnimator(m_strMove))
            {
                Logs.LogW("Can't stop move animation.Don't have parameter " + m_strMove + " in Animator");
            }
            else
            {
                m_anim.SetBool(m_strMove, false);
            }
            //Look to center
            if (transform.position.x > Camera.main.transform.position.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.position.x < Camera.main.transform.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            m_stage = StagesBoss.idle;
        }
    }

    public virtual void Skill()
    {
        isDoing = true;
        if (CheckParemeterInAnimator(m_skills[SkillIndex]))
        {
            Logs.LogW("Can't do skill.Don't have parameter " + m_skills[SkillIndex] + " in Animator");
            return;
        }
        m_anim.SetBool(m_skills[SkillIndex], true);
    }
    public void EndAnimatioin()
    {
        if (CheckParemeterInAnimator(m_skills[SkillIndex]))
        {
            Logs.LogW("Can't end skill.Don't have parameter " + m_skills[SkillIndex] + " in Animator");
        }
        else
        {
            m_anim.SetBool(m_skills[SkillIndex], false);
            SkillIndex = Random.Range(0, m_skills.Count);
        }
        m_stage = StagesBoss.move;
        index = Random.Range(0, m_Points.Count);
        m_distanceMove = Vector2.Distance(transform.position, m_Points[index].position);
        isDoing = false;
    }
    public bool CheckParemeterInAnimator(string Param)
    {
        foreach (AnimatorControllerParameter para in GetComponent<Animator>().parameters)
        {
            if (para.name == Param)
            {
                return false;
            }
        }
        return true;
    }
}
