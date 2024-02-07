using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform originalParent;

    public CardObject cardObject;

    public Text nameText;
    public Text descriptionText;
    
    void Start()
    {
        nameText.text = cardObject.cardName;
        descriptionText.text = cardObject.description;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = this.transform.parent;
        this.transform.SetParent(this.transform.root);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(originalParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
