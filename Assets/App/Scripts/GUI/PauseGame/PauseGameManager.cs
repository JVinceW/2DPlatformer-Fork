using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameManager : MonoBehaviour
{
    #region Singleton
    public static PauseGameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Logs.LogW("More than one Pause Game Manager");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject PauseUI;
    bool m_isPause;

    public bool isPause
    {
        get => m_isPause;
    }

    private void Start()
    {
        PauseUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(PlayerManager.instance.kPause))
        {
            Pause(!m_isPause);
        }
    }
    private void FixedUpdate()
    {
        if(m_isPause)
        {
            Time.timeScale = 0;
        }
    }

    public void Pause(bool isPause)
    {
        m_isPause = isPause;
        Time.timeScale = m_isPause ? 0f:1f;
        PauseUI.SetActive(isPause);
    }

    public void SoundEffect()
    {

    }

    public void SoundBG()
    {

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        m_isPause = false;
        PauseUI.SetActive(false);
    }

    public void SaveGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
