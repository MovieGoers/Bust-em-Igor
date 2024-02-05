using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public Text hpText;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetHPText(int level, float exp, float damage, float speed, float maxHP, float hp, float attackTime, float attackTimer)
    {
        hpText.text = "";
        hpText.text += "Lv : " + level + '\n';
        hpText.text += "EXP : " + exp + '\n';
        hpText.text += "DMG : " + damage + '\n';
        hpText.text += "SPD : " + speed + '\n';
        hpText.text += "HP : " + hp.ToString("F2") + " / " + maxHP.ToString("F2") + '\n';
        hpText.text += "ATK : " + attackTimer.ToString("F2") + " / " + attackTime.ToString("F2") + '\n';
    }
}
