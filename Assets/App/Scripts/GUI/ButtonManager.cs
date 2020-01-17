using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    static public int m_CurrentIndex=0;
    public int m_MaxIndex;
    bool m_isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Vertical");
        if (Input.GetAxis("Vertical") != 0)
        {
            if(!m_isPressed)
            {
                if (input < 0)
                {
                    m_CurrentIndex++;
                    m_CurrentIndex = m_CurrentIndex % m_MaxIndex;
                }
                else if (input > 0)
                {
                    m_CurrentIndex--;
                    if (m_CurrentIndex < 0)
                        m_CurrentIndex = m_MaxIndex - 1;
                }
                m_isPressed = true;
            }
        }
        else
        {
            m_isPressed = false;
        }
    }
}
