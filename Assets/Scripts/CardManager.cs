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

    public void ActivateCardEffect(int cardID, float value)
    {
        switch (cardID)
        {
            case 1:
                HealPlayer(value);
                break;
            default:
                break;
        }
    }

    void HealPlayer(float value)
    {
        GameManager.Instance.PlayerGameObject.GetComponent<PlayerScript>().hp += value;
    }
}
