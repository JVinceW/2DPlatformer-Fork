using UnityEngine;

public class PlayerManager
{
    static PlayerManager m_instance;
    public KeyCode Jump;
    public KeyCode Fire;
    public KeyCode NextWeapon;
    public KeyCode PreWeapon;
    public KeyCode kInventory;
    public KeyCode kInfomation;
    public KeyCode kDash;
    //Skill
    public KeyCode skill1;
    public KeyCode skill2;
    public KeyCode skill3;
    public KeyCode skill4;
    //UI
    public KeyCode kPause;
    public static PlayerManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new PlayerManager();
            return m_instance;
        }
        set
        {
            m_instance = value;
        }
    }

    PlayerManager()
    {
        //Map key
        Jump = KeyCode.Space;
        Fire = KeyCode.F;
        NextWeapon = KeyCode.E;
        PreWeapon = KeyCode.Q;
        kInventory = KeyCode.B;
        kInfomation = KeyCode.C;
        kDash = KeyCode.M;

        //skill
        skill1 = KeyCode.Alpha1;
        skill2 = KeyCode.Alpha2;
        skill3 = KeyCode.Alpha3;
        skill4 = KeyCode.Alpha4;

        //UI
        kPause = KeyCode.Escape;
    }

    public Vector3 GetPostition()
    {
        GameObject player = GameObject.FindGameObjectWithTag(GetPlayerTag());
        if (player == null)
        {
            Logs.LogE("Can't get player gameObject");
            return Vector3.one;
        }
        return player.transform.position;
    }

    public string GetPlayerTag()
    {
        return "Player";
    }

    public string GetWeaponsPosition()
    {
        return "WeaponPos";
    }

    public string GetPlayerAttackTags()
    {
        return "PlayerAttack";
    }

    public GameObject GetPlayerGameObject()
    {
        GameObject player = GameObject.FindGameObjectWithTag(GetPlayerTag());
        if (player == null)
        {
            Logs.LogE("Can't get player gameObject");
            return null;
        }
        return player;
    }

    public IAttributes GetPlayerAttributes()
    {
        if (PlayerAttribute.instance != null)
            return PlayerAttribute.instance;
        else
        {
            Logs.LogE("Player attribute is null");
            return null;
        }
    }

    public float GetPlayerCrits()
    {
        return Random.Range(1f, 2f);
    }

    public void SetAttack(IAttributes attributes)
    {
        //Logs.LogE("null set attack in PlayerManager line 82");
        PlayerAttribute.instance.TakeDamage(attributes);
    }

    public void AddForce(Vector3 EnemyPos)
    {
        GameObject player = GameObject.FindGameObjectWithTag(GetPlayerTag());
        Vector3 playerPos = player.transform.position;
        Vector3 dir = EnemyPos - playerPos;
        dir = -dir.normalized;
        player.GetComponentInParent<Rigidbody2D>().AddForce(dir * 500f);
    }
    
}
