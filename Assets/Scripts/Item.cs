using System;
using UnityEngine;

public enum ItemCategory
{
    Resource,
    Weapon,
    Armour,
    Consumable
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;
    public ItemCategory category;

    [Header("Stackabel Setting")]
    public bool isStackable;
    public int maxStack = 64;
}
