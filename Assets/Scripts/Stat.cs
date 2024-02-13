using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    private List<float> mods = new List<float>();

    public float GetValue()
    {
        float finalValue = baseValue;
        mods.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddMod(float mod)
    {
        mods.Add(mod);
    }

    public void RemoveMod(float mod)
    {
        mods.Remove(mod);
    }
}
