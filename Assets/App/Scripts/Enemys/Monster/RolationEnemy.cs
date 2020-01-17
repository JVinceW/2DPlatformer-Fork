using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolationEnemy : MonoBehaviour
{
    public float m_speed;
    public float m_damage;
    private Rigidbody2D m_rdBody;
    // Start is called before the first frame update
    void Start()
    {
        m_rdBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, m_speed), Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == PlayerManager.instance.GetPlayerTag())
        {
            StartCoroutine(DamagePerSecond());
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == PlayerManager.instance.GetPlayerTag())
    //    {
    //        StartCoroutine(DamagePerSecond());
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    IEnumerator DamagePerSecond()
    {
        yield return new WaitForSeconds(1);
        //PlayerManager.instance.SetAttack(m_damage);
        StartCoroutine(DamagePerSecond());
    }
}
