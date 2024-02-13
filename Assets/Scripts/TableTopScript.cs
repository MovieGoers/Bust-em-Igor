using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableTopScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();

        if (card != null)
        {
            if (cardScript.cardObject.cardType == Type.consumable)
            {
                CardManager.Instance.ActivateCardEffect(cardScript.cardObject.cardId, cardScript.cardObject.effectValue);
                Destroy(card);
            }
        }
    }
}
