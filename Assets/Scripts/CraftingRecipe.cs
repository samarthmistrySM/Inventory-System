using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Input Slots (exact order matters)")]
    public List<Item> inputs = new List<Item>(); //for inferedients

    [Header("Output")]
    public Item resultItem; //for output item
    public int resultQuantity = 1; //for output items's quantity
}
