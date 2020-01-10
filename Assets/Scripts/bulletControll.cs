using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControll : MonoBehaviour
{
    public enum TagToCheck { Player, Enemy};
    public enum TypeBullet { Normal, Position};
    public float Speed;
    public TagToCheck Tag;
    public TypeBullet m_TypeBullet;
    public GameObject m_OnHit;
    private float m_Damage=0;
    Rigidbody2D m_rbBody;

    //Postion
    Vector2 StartPoint;
    Vector2 EndPoint;
    Vector2 ConTrolPoint;
    //float ControlPointX;
    //float ControlPointY;
    //float CurveX;
    //float CurveY;
    //float BezierTime = 0;
    //End Position


    private void Start()
    {
        m_rbBody = GetComponent<Rigidbody2D>();
        //if (m_TypeBullet == TypeBullet.Position)
        //{
        //    StartPoint = transform.position;
        //    RaycastHit2D groundInfo = Physics2D.Raycast(PlayerManager.instance.GetPostition(), Vector2.down, 5f);
        //    EndPoint = PlayerManager.instance.GetPlayerGameObject().GetComponent<Renderer>().bounds.min;
        //    //PlayerManager.instance.GetPlayerGameObject().GetComponent<Renderer>().bounds.min;
        //    ControlPointX = (StartPoint.x + PlayerManager.instance.GetPostition().x) / 2f;
        //    ControlPointY = Mathf.Abs(Vector2.Distance(StartPoint, EndPoint))/2;
        //    ConTrolPoint = new Vector2(ControlPointX, ControlPointY);
        //    //Logs.LogD("ControlPoint: X =" + ConTrolPoint.x + " - Y =" + ConTrolPoint.y);
        //}
        //if(m_TypeBullet == TypeBullet.Normal)
        //{
        m_rbBody.velocity = transform.right * Speed;
        //}
    }

    private void Update()
    {
        //if(m_TypeBullet == TypeBullet.Position)
        //{
        //    /// Start Position
        //    BezierTime = BezierTime + (Time.deltaTime / Speed);

        //    if (BezierTime >= 1)
        //    {
        //        //m_rbBody.velocity = transform.right * Speed;
        //        return;
        //    }

        //    CurveX = (((1 - BezierTime) * (1 - BezierTime)) * StartPoint.x) + (2 * BezierTime * (1 - BezierTime) * ConTrolPoint.x) + ((BezierTime * BezierTime) * EndPoint.x);
        //    CurveY = (((1 - BezierTime) * (1 - BezierTime)) * StartPoint.y) + (2 * BezierTime * (1 - BezierTime) * ConTrolPoint.y) + ((BezierTime * BezierTime) * EndPoint.y);
        //    transform.position = new Vector3(CurveX, CurveY, 0);
        //    ///End Position
        //}
    }

    public void SetDame(float Damage)
    {
        m_Damage = Damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Tag == TagToCheck.Player)
        {
            if (other.tag == PlayerManager.instance.GetPlayerTag())
            {
                //PlayerManager.instance.SetAttack(m_Damage);
                Destroy(this.gameObject);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != PlayerManager.instance.GetPlayerTag() && collision.gameObject.tag != "PlayerGround")
        {
            Logs.LogD(collision.gameObject.tag);
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(HideCollider());
        }
    }
    IEnumerator HideCollider()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
