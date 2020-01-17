using UnityEngine;
using TMPro;

public class InformationUI : MonoBehaviour
{
    public TextMeshProUGUI m_Heath;
    public TextMeshProUGUI m_Attack;
    public TextMeshProUGUI m_Defense;
    public TextMeshProUGUI m_Crit;

    private void Start()
    {
        PlayerAttribute.instance.OnAttributeChangeCallBack += OnEquipmentChange;
        SetInfomation();
    }

    void OnEquipmentChange()
    {
        SetInfomation();
    }

    void SetInfomation()
    {
        int Heath = (int)PlayerAttribute.instance.Heath.GetValue();
        int CurrentHeath = (int)PlayerAttribute.instance.GetCurrentHeath();
        int Attack = (int)PlayerAttribute.instance.Damage.GetValue();
        int Defense = (int)PlayerAttribute.instance.Defense.GetValue();
        int Crit = (int)PlayerAttribute.instance.Crit.GetValue();

        SetText(Heath, CurrentHeath, Attack, Defense, Crit);
    }

    void SetText(int heath,int CurrentHeath, int attack, int defense, int crit)
    {
        m_Heath.text = "Heath : " + CurrentHeath + "/" + heath;
        m_Attack.text = "Attack : " + attack;
        m_Defense.text = "Armor : " + defense;
        m_Crit.text = "Crit : " + crit;
    }
}
