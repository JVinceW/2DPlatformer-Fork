using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionScript : MonoBehaviour
{
    public OptionsAction m_action;
    public int m_buttonIndex;
    public Transform m_PositionCurso;
    public TextMeshProUGUI m_value;
    List<string> m_lstValue;
    Resolution[] m_resolutions;
    int m_valueIndex = 0;
    bool isPress = false;

    static public OptionScript instance;

    public enum OptionsAction
    {
        Graphic,
        FullScreen,
        Quality
    }
    private void Awake()
    {
        if(instance != null)
        {
            Logs.LogW("More than OptionScript");
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_lstValue = new List<string>();
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_buttonIndex == ButtonManager.m_CurrentIndex)
        {
            GameObject.FindGameObjectWithTag("Cursor").transform.position = m_PositionCurso.localPosition;
            float Horizontal = Input.GetAxis("Horizontal");
            if (Horizontal != 0)
            {
                if (!isPress)
                {
                    if (Horizontal > 0)
                    {
                        m_valueIndex++;
                        m_valueIndex = m_valueIndex % m_lstValue.Count;

                    }
                    else if (Horizontal < 0)
                    {
                        m_valueIndex--;
                        if (m_valueIndex < 0)
                            m_valueIndex = m_lstValue.Count - 1;
                    }
                    Onchange();
                }
                isPress = true;
            }
            else
            {
                isPress = false;
            }
        }
    }

    void Initialized()
    {
        if(m_action == OptionsAction.Graphic)
        {
            m_lstValue.Clear();
            m_resolutions = new Resolution[3];
            //1280x720
            m_resolutions[0].width = 1280;
            m_resolutions[0].height = 720;
            m_resolutions[0].refreshRate = 60;
            //
            m_resolutions[1].width = 1366;
            m_resolutions[1].height = 768;
            m_resolutions[1].refreshRate = 60;
            //
            m_resolutions[2].width = 1920;
            m_resolutions[2].height = 1080;
            m_resolutions[2].refreshRate = 60;
            //
            m_valueIndex = 0;
            //m_resolutions = Screen.resolutions;
            for (int i = 0; i < m_resolutions.Length; i++)
            {
                m_lstValue.Add(m_resolutions[i].width + " x " + m_resolutions[i].height);
                //if (m_resolutions[i].width == Screen.currentResolution.width &&
                //    m_resolutions[i].height == Screen.currentResolution.height)
                //{
                //    m_valueIndex = i;
                //}
            }
            m_value.text = m_lstValue[m_valueIndex];
            return;
        }
        if(m_action == OptionsAction.FullScreen)
        {
            m_lstValue.Clear();
            m_lstValue.Add("ON");
            m_lstValue.Add("OFF");
            m_valueIndex = Screen.fullScreen == true ? 0 : 1;
            m_value.text = m_lstValue[m_valueIndex];
            return;
        }
        if(m_action == OptionsAction.Quality)
        {
            m_lstValue.Clear();
            m_lstValue.Add("VERY LOW");
            m_lstValue.Add("LOW");
            m_lstValue.Add("MEDIUM");
            m_lstValue.Add("HIGHT");
            m_valueIndex = QualitySettings.GetQualityLevel();
            m_value.text = m_lstValue[m_valueIndex];
            return;
        }
    }

    public void Onchange()
    {
        if (m_action == OptionsAction.Graphic)
        {
            Screen.SetResolution(m_resolutions[m_valueIndex].width, m_resolutions[m_valueIndex].height, Screen.fullScreen);
        }
        if (m_action == OptionsAction.FullScreen)
        {
            Screen.fullScreen = m_valueIndex == 0 ? true: false ;
        }
        if (m_action == OptionsAction.Quality)
        {
            QualitySettings.SetQualityLevel(m_valueIndex);
        }
        m_value.text = m_lstValue[m_valueIndex];
    }
}
