using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    [SerializeField]
    private int baseValue;

    public int GetValue()
    {
        return baseValue;
    }
}
