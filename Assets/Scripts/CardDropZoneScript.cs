using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropZoneScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // eventData.pointerDrag : 드래그된 아이템 / gameObject : 드래그한 곳
        Debug.Log(eventData.pointerDrag.name + " was Dropped to " + gameObject.name);

        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();


        //cardScript.originalParent = this.transform;
        if (card != null)
        {
            if (gameObject.CompareTag("TableTop"))
            {
                if(cardScript.cardObject.cardType == Type.consumable)
                {
                    CardManager.Instance.ActivateCardEffect(cardScript.cardObject.cardId, 10f);
                    Destroy(card);
                }
            }
            else if (gameObject.CompareTag("Trashbin"))
            {
                Destroy(card);
            }
        }
    }
}
