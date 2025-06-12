
using UnityEngine;

public class TarrotItem : Item
{
    protected override void Awake()
    {
        id = gameObject.name;
        itemName = gameObject.name;
        itemIcon = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    protected override void OnUse()
    {
    }
}
