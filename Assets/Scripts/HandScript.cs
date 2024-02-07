using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject card = eventData.pointerDrag;
        CardScript cardScript = card.GetComponent<CardScript>();
        // do something.
    }

    void Update()
    {
    }
}
