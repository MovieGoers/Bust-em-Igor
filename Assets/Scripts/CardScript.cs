using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform originalParent;

    [HideInInspector]
    public enum Type { 
        consumable,
        durable
    };

    public string cardName;
    public Type cardType;
    public Sprite cardImage;
    [TextArea]
    public string description;

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

    public void ActivateCardEffect()
    {
        Debug.Log("Card Activated");
        //GameManager.Instance.PlayerGameObject.GetComponent<PlayerScript>().IncreaseDamage(10f);
    }
}
