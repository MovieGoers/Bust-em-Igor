using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour, IDropHandler
{
    int cardsCount;
    List<GameObject> cards = new List<GameObject>();


    public void OnDrop(PointerEventData eventData)
    {
        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();
        // do something.

        ResetCardEffectsOnHands();
        GetAllCardGameObjects();
        ActivateCardsOnHand();
    }

    void Start()
    {
        ResetCardEffectsOnHands();
        GetAllCardGameObjects();
        ActivateCardsOnHand();
    }

    void ResetCardEffectsOnHands()
    {
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardScript>().cardObject.cardType == Type.Fixed)
            {
                Debug.Log("Reset the card : " + card.name);
            }
        }
    }

    void GetAllCardGameObjects()
    {
        cardsCount = transform.childCount;
        for (int i =0;i< cardsCount; i++)
        {
            cards.Add(transform.GetChild(i).gameObject);
        }
        
    }

    // Hand 에 있는 모든 고정 카드 효과를 발동.
    void ActivateCardsOnHand()
    {
        foreach(GameObject card in cards)
        {
            if(card.GetComponent<CardScript>().cardObject.cardType == Type.Fixed)
            {
                CardScript cardScript = card.GetComponent<CardScript>();
                Debug.Log("Activate the card ID : " + cardScript.cardObject.cardId);
                CardManager.Instance.ActivateCardEffect(cardScript.cardObject.cardId, cardScript.cardObject.effectValue);
            }
        }
    }
}
