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

        if(card != null)
        {
            card.GetComponent<CardScript>().originalParent = this.transform;

            if (gameObject.CompareTag("TableTop"))
            {
                card.GetComponent<CardScript>().ActivateCardEffect();
            }
            else if (gameObject.CompareTag("Trashbin"))
            {
                Destroy(card);
            }
        }
    }
}
