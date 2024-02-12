using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP { get; private set; }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if(currentHP <= 0f)
        {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " Dies");
    }
}
