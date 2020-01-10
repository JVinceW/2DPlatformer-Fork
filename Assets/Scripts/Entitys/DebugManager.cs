using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [Header("Config")]
    public bool Cheat;
    public GameObject DemoWeapont;
    public static DebugManager instance;
    private bool m_isDebug = false;
    private bool m_DebugMenuIsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gui = GameObject.FindGameObjectsWithTag(this.tag);
        if (gui.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //ShowDebug with key
        if(Input.GetKeyDown(KeyCode.F10))
        {
            m_isDebug = !m_isDebug;
            string strlog = m_isDebug == true ? "Debug on!" : "Debug Off";
            Logs.LogD(strlog);
        }
        //Debug menu
        if(Input.GetKeyDown(KeyCode.F11))
        {
            m_DebugMenuIsOpen = !m_DebugMenuIsOpen;
        }
        //Weapons
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (Cheat)
            {
                if (DemoWeapont == null)
                {
                    Logs.LogE("Dont have Demo weapon in DebugManager line 50");
                    return;
                }
                WeaponsManager.instance.AddWeapon(DemoWeapont);
                Logs.LogD("Success Demo weapon in DebugManager line 54");

            }
            else
            {
                Logs.LogE("Can not Add weapon because cheat is OFF");
            }
        }
        //Test Diaglog
        if(Input.GetKeyDown(KeyCode.T))
        {
            List<string> lstTalk = new List<string>();
            lstTalk.Add(" Ví dụ về unicode chat.if you approach too fast, AUT will not respond to that event when internet not working.");
            lstTalk.Add("Note 5: After capping reached, a text explaining no more ads to watch will appear, exit and re enter the section so that watch button appear");
            DiaglogManager.instance.Talk("Tinu", lstTalk);
        }
    }
    public bool isDebug()
    {
        return m_isDebug;
    }
    private void Debuging()
    {
        m_isDebug = !m_isDebug;
    }
    private void Cheating()
    {
        Cheat = !Cheat;
    }
    void OnGUI()
    {
        if (m_DebugMenuIsOpen)
        {
            const float height = 100;

            GUILayout.BeginArea(new Rect(30, Screen.height - height, 200, height));

            GUILayout.BeginVertical("box");
            GUILayout.Label("Press F11 to close");

            bool meleeAttackEnabled = GUILayout.Toggle(Cheat, "Cheating");
            bool rangeAttackEnabled = GUILayout.Toggle(m_isDebug, "Debug");
            if (meleeAttackEnabled != Cheat)
            {
                if (meleeAttackEnabled)
                    Cheat = true;
                else
                    Cheat = false;
            }

            if (rangeAttackEnabled != m_isDebug)
            {
                if (rangeAttackEnabled)
                    m_isDebug = true;
                else
                    m_isDebug = false;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
            //Show keyTutorial
            GUILayout.BeginArea(new Rect(250, Screen.height - height, 200, height));

            GUILayout.BeginVertical("box2");

            GUILayout.Label("Press F12 To add demo weapons");
            GUILayout.Label("Press F10 To show Debug");

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }
    }
}
