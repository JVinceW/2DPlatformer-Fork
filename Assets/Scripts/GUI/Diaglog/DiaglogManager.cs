using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiaglogManager : MonoBehaviour
{
    #region Singleton
    public static DiaglogManager instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion


    public enum TalkStage { Skip, Next, OK};

    public TextMeshProUGUI m_Text;
    public Button m_SkipButton;
    public Image m_BG;
    public List<string> m_lstSpeak;
    public string m_Talker;
    public float m_speed;
    public TalkStage m_stage;
    int index = 0;
    int CurrentText = 0;

    Text m_ButtonText;

    void Start()
    {
        m_Text.text = "";
        m_Text.enabled = false;
        m_BG.enabled = false;
        m_ButtonText = m_SkipButton.GetComponentInChildren<Text>();
        m_ButtonText.text = "Skip...";
        ButtonSkip(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    m_Text.enabled = true;
        //    m_Text.text = m_Talker + ": ";
        //    m_BG.enabled = true ;
        //    StartCoroutine(Speak());
        //    ButtonSkip(true);
        //}
    }
    IEnumerator Speak()
    {
        m_Text.text += m_lstSpeak[CurrentText][index];
        yield return new WaitForSeconds(m_speed);
        index++;
        if(index < m_lstSpeak[CurrentText].Length)
        {
            StartCoroutine(Speak());
        }
        else
        {
            SkipButton();
        }
    }

    void ButtonSkip(bool isEnable)
    {
        m_SkipButton.interactable = isEnable;
        m_ButtonText.enabled = isEnable;
    }

    public void Talk(string strName, List<string> Talk)
    {
        m_stage = TalkStage.Skip;
        m_Text.enabled = true;
        m_BG.enabled = true;
        m_Talker = strName;

        if (m_lstSpeak == null)
            m_lstSpeak = new List<string>();
        m_lstSpeak = Talk;
        m_Text.text = m_Talker + ": ";
        CurrentText = 0;
        index = 0;
        StartCoroutine(Speak());
        ButtonSkip(true);
    }
    public void SkipButton()
    {
        switch(m_stage)
        {
            case TalkStage.Skip: Skip();break;
            case TalkStage.Next: Next();break;
            case TalkStage.OK: End(); break;
        }
    }
    void Skip()
    {
        StopAllCoroutines();
        m_Text.text = m_Talker + ": " + m_lstSpeak[CurrentText];
        CurrentText++;
        if (CurrentText >= m_lstSpeak.Count)
        {
            m_ButtonText.text = "OK...";
            m_stage = TalkStage.OK;
        }
        else
        {
            m_ButtonText.text = "Next...";
            m_stage = TalkStage.Next;
        }
    }
    void Next()
    {
        m_Text.text = m_Talker + ": ";
        index = 0;
        m_ButtonText.text = "Skip...";
        StartCoroutine(Speak());
        m_stage = TalkStage.Skip;
    }
    void End()
    {
        m_BG.enabled = false;
        ButtonSkip(false);
        m_ButtonText.enabled = false;
        m_Text.enabled = false;
    }
}
