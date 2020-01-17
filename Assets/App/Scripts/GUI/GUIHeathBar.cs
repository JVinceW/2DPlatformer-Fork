using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIHeathBar : MonoBehaviour
{
    public Image m_HeathBar;
    public TextMeshProUGUI m_PlayerName;
    public PlayerAttribute playerAttribute;
    private void Start()
    {
        if(playerAttribute != null)
            m_PlayerName.text = playerAttribute.Name;
    }
    private void LateUpdate()
    {
        if (playerAttribute != null)
            UpdateHeathBar(playerAttribute.GetHealthPercent());
    }
    void UpdateHeathBar(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        m_HeathBar.transform.localScale = new Vector3(value, 1f, 1f);
    }
}
