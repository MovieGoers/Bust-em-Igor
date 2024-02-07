using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static CardManager instance;

    public static CardManager Instance
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

    public void ActivateCardEffect(int cardID)
    {
        switch (cardID)
        {
            case 1:
                GameManager.Instance.PlayerGameObject.GetComponent<PlayerScript>().HealPlayer(50f);
                break;
            case 2:
                GameManager.Instance.PlayerGameObject.GetComponent<PlayerScript>().IncreaseDamage(20f);
                break;
            default:
                break;
        }
    }
}
