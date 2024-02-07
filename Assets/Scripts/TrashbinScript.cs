using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashbinScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();

        if (card != null)
        {
            Destroy(card);
        }
    }
}
