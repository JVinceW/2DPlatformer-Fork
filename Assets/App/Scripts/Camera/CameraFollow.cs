using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform m_target;
    private float height, width;

    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag(PlayerManager.instance.GetPlayerTag()).transform;
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet" || collision.tag == "BulletEnemy")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == PlayerManager.instance.GetPlayerTag()) 
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void LateUpdate()
    {
        if (m_target == null)
            return;
        transform.position = new Vector3(Mathf.Clamp(m_target.position.x, xMin, xMax), Mathf.Clamp(m_target.position.y, yMin, yMax), transform.position.z);
    }

    public void SetMaxY(float maxY)
    {
        yMax = maxY - height/2;
    }
    public void SetMaxX(float maxX)
    {
        
        xMax = maxX - width/2;
    }
    public void SetMinY(float MinY)
    {
        yMin = MinY + height/2;
    }
    public void SetMinX(float minX)
    {
        xMin = minX + width/2;
    }
}
