using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int NextLevel = 0;
    bool m_CanNext = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (m_CanNext)
            {
                SceneLoad.instance.LoadScreen(NextLevel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == PlayerManager.instance.GetPlayerTag())
        {
            m_CanNext = true;
        }
    }
}
