using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEffects : MonoBehaviour
{
    public GameObject bullet;
    public void InitBullet()
    {
        if(bullet == null)
        {
            Logs.LogE("Bullet is null, can create");
        }
        GameObject bull = Instantiate(bullet,transform.position,Quaternion.Inverse(transform.rotation));
        bull.transform.position = new Vector3(transform.position.x - 0.55f, transform.position.y + 0.28f, 0);
    }
}
