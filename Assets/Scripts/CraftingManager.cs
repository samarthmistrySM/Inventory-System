using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<CraftingRecipe> recipes; //List of all craftable items with its recipe
    [SerializeField] private List<ItemSlot> inputSlots; //inputslots as ingredients for crafting
    [SerializeField] private ItemSlot outputSlot; //output slot for item which is crafted

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //works on c pressed
            TryCraft();
        }
    }

    private bool MatchesRecipe(CraftingRecipe recipe)
    {
        if (recipe.inputs.Count != inputSlots.Count)
            return false;
        //using for loop for matching the perfact slot with recipe ingreditents
        for (int i = 0; i < inputSlots.Count; i++)
        {
            Item required = recipe.inputs[i];
            Item inSlot = inputSlots[i].item;

            //matching requied item and currenlty having item

            if (required == null && inSlot == null) continue; //if the item is null and the recipe also not having continue
            //else not matched should be false
            if (required == null && inSlot != null) return false;
            if (required != null && inSlot == null) return false;
            if (required != inSlot) return false;
        }
        return true;
    }

    private void Craft(CraftingRecipe recipe)
    {
        for (int i = 0; i < recipe.inputs.Count; i++)
        {
            if (recipe.inputs[i] != null)
            {
                ItemSlot slot = inputSlots[i];
                slot.quantity -= 1; //if crafted remove one quantity as item used in crafted item

                if (slot.quantity <= 0) slot.ClearSlot(); //if only one quantity than clear the slot
                else slot.UpdateUI(); //updating the slot
            }
        }
        outputSlot.AddItem(recipe.resultItem, recipe.resultQuantity); //add crafted item to output slot
    }

    public void TryCraft()
    {
        //find the perfact matched recipe as per the input items
        foreach (var recipe in recipes)
        {
            if (MatchesRecipe(recipe))
            {
                Craft(recipe);
                return;
            }
        }
        Debug.Log("recipe not found!");
    }

    public void ReturnInputsToInventory()
    {
        //menu get closed without picking items than return items back to inventory will work on disbale
        foreach (var slot in inputSlots)
        {
            if (slot.item != null && slot.quantity > 0)
            {
                InventoryManager.Instance.AddItemToInventory(slot.item, slot.quantity);
                slot.ClearSlot();
            }
        }
    }

    void OnDisable()
    {
        ReturnInputsToInventory();
    }
}
