using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public float Value;
    private List<float> modifiers = new List<float>();

    // Add all modifiers together and return the result
    public float GetValue()
    {
        float finalValue = Value;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    // Add a new modifier to the list
    public void AddModifier(float modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    // Remove a modifier from the list
    public void RemoveModifier(float modifier)
    {
        if (modifier != 0 && modifiers.Count > 0)
            modifiers.Remove(modifier);
    }

}
