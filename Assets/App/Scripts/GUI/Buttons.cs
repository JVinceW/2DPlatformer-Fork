using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{
    public ActionButton m_action;
    public int m_ButtonIndex;
    public Transform m_PositionCurso;

    //GUI
    [Header("GUI Config")]
    public GameObject m_objMenu = null;
    public GameObject m_objOption = null;
    Animator m_Anim;
    public enum ActionButton
    {
        Start,
        Option,
        Quit,
        Default,
        Back
    }
    private void Start()
    {
        m_Anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_ButtonIndex == ButtonManager.m_CurrentIndex)
        {
                GameObject.FindGameObjectWithTag("Cursor").transform.position = m_PositionCurso.localPosition;
                m_Anim.SetBool("Press", true);
                if (Input.GetButtonDown("Submit"))
                {
                    m_Anim.SetTrigger("Select");
                    m_Anim.SetBool("Press", false);
                    Logs.LogD("Enter:" + m_ButtonIndex);
                    switch (m_action)
                    {
                        case ActionButton.Start: SceneLoad.instance.LoadScreen(1); break;
                        case ActionButton.Option: Options(); break;
                        case ActionButton.Quit: Application.Quit(); break;
                        //case ActionButton.Default: Default(); break;
                        case ActionButton.Back: Back(); break;
                    }
                }
        }
        else
        {
            m_Anim.SetBool("Press", false);
        }
    }

    void Options()
    {
        if(m_objOption == null || m_objMenu == null)
        {
            Logs.LogD("Option: Menu and Option can't null : Button.cs line 63");
            return;
        }
        m_objMenu.SetActive(false);
        m_objOption.SetActive(true);
        ButtonManager.m_CurrentIndex = 0;
    }
    void Back()
    {
        if (m_objOption == null || m_objMenu == null)
        {
            Logs.LogD("Back: Menu and Option can't null : Button.cs line 83");
            return;
        }
        m_objMenu.SetActive(true);
        m_objOption.SetActive(false);
        ButtonManager.m_CurrentIndex = 0;
    }
}
