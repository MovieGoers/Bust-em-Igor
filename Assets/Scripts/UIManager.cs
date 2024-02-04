using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public Text hpText;
    public Text AttackText;
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

    public void SetHPText(float hp)
    {
        hpText.text = "HP : " + hp.ToString("F2"); ;
    }

    public void SetAttackText(float time)
    {
        AttackText.text = "Attack : " + time.ToString("F2");
    }
}
