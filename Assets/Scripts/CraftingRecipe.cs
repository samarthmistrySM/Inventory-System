using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [Header("Input Slots (exact order matters)")]
    public Item[] inputs = new Item[3]; // slot0, slot1, slot2

    [Header("Output")]
    public Item resultItem;
    public int resultQuantity = 1;
}
