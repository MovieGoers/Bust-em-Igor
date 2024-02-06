using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropZoneScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // eventData.pointerDrag : �巡�׵� ������ / gameObject : �巡���� ��
        Debug.Log(eventData.pointerDrag.name + " was Dropped to " + gameObject.name);

        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();


        //cardScript.originalParent = this.transform;
        if (card != null)
        {
            if (gameObject.CompareTag("TableTop"))
            {
                if(cardScript.cardType == CardScript.Type.consumable)
                {
                    card.GetComponent<CardScript>().ActivateCardEffect();
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
