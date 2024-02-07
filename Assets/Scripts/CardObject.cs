using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    consumable,
    durable
};

[CreateAssetMenu(fileName = "default card", menuName = "Card Object")]
public class CardObject : ScriptableObject
{
    public int cardId;
    public string cardName;
    public Type cardType;
    public Sprite cardImage;
    public string description;
}
