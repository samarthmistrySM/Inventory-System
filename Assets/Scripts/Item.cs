using System;
using UnityEngine;

//enum for item categories to classify different types of items
public enum ItemCategory
{
    Resource, //resources like wood, stone, ore
    Weapon, //weapons like sword, bow, gun
    Armour, //armour like helmet, chestplate
    Consumable //consumables like food, potion
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName; //name of the item
    public Sprite icon; //UI icon of the item
    public ItemCategory category;  //category of the item (enum above)

    [Header("Stackabel Setting")]
    public bool isStackable; //check if item can be stacked
    public int maxStack = 64; //maximum stack size if stackable
}
