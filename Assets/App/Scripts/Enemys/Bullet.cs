using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Diriction;
    public float Speed;
    //public GameObject DestroyEffect;
    float m_Damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y, transform.position.z);
        this.gameObject.transform.position = pos;
    }

    public void SetDamage(float damage)
    {
        m_Damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == PlayerManager.instance.GetPlayerTag())
        {
            Destroy(gameObject);
            //Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            //PlayerManager.instance.SetAttack(m_Damage);
        }
    }
}
