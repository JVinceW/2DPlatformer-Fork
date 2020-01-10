using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public GameObject objbullet;
    private Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        Transform Trs_player = GameObject.Find(PlayerManager.instance.GetWeaponsPosition()).transform;
        pos = Trs_player.transform;
        this.transform.rotation = Trs_player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PlayerManager.instance.Fire))
        {
            if(objbullet == null)
            {
                Logs.LogE("Dont have gameobject bullet in Weapons Controller line 21");
                return;
            }
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        pos = GameObject.Find(PlayerManager.instance.GetWeaponsPosition()).transform;
        transform.position = Vector3.MoveTowards(transform.position, pos.position, 1f);
        transform.rotation = pos.rotation;
    }

    private void Shoot()
    {
        GameObject ibullet = Instantiate(objbullet, this.transform.position, this.transform.rotation);
        ibullet.GetComponent<bulletControll>().SetDame(0);
    }
}
